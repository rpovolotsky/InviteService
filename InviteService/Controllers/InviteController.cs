using InviteService.Models;
using InviteService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InviteService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InviteController : ControllerBase
    {
        private readonly ISettings _settings;
        private readonly IPhoneValidator _phoneValidator;
        private readonly IInviteRepository _inviteRepository;
        private readonly ISmsMessageValidator _smsMessageValidator;
        private readonly ISmsService _smsService;
        private readonly IDateTimeService _dateTimeService;
        private readonly ILogger<InviteController> _logger;
        private readonly ITranslitService _translitService;
        public InviteController(ISettings settings, IDateTimeService dateTimeService, IPhoneValidator phoneValidator, IInviteRepository inviteRepository, ISmsMessageValidator smsMessageValidator, ISmsService smsService, ILogger<InviteController> logger, ITranslitService translitService)
        {
            _settings = settings;
            _phoneValidator = phoneValidator;
            _inviteRepository = inviteRepository;
            _dateTimeService = dateTimeService;
            _smsMessageValidator = smsMessageValidator;
            _smsService = smsService;
            _logger = logger;
            _translitService = translitService;

        }

        /// <summary>
        /// Отправка СМС-приглашения на список телефонных номеров для подключения к Системе.
        /// </summary>
        /// <param name="phones">Список номеров телефонов разделитель ;</param>
        /// <param name="message">Сообщение</param>
        /// <param name="apiId">Идентификатор приложения</param>
        /// <returns></returns>
        [HttpPost("send")]
        public async Task<IActionResult> Send([FromForm]string phones, [FromForm] string message, [FromForm] int apiId = 4)
        {
            //Проверка номеров
            if (String.IsNullOrEmpty(phones))
            {
                return StatusCode(400, new {code = 401, 
                                            error = "BAD_REQUEST PHONE_ NUMBERS_EMPTY", 
                                            message = "Phone numbers are missing" });
            }
            
            var phoneList = phones.Split(";");
            if (phoneList.Length > _settings.MaxCountInviteSend)
            {
                return StatusCode(400, new { code = 402, 
                                             error = "BAD_REQUEST PHONE_NUMBERS_INVALID", 
                                             message = "Too much phone numbers, should be less or equal to 16 per request" });
            }

            if (phoneList.Any(r => !_phoneValidator.IsValid(r)))
            {
                return StatusCode(400, new { code = 400, 
                                             error = "BAD_REQUEST PHONE_NUMBERS_INVALID", 
                                             message = "One or several phone numbers do not match with international format" });
            }

            var phonesDublicate = phoneList.GroupBy(r => r).Where(r => r.Count() > 1).Select(r => r.Key);
            if (phonesDublicate.Count() > 0)
            {
                return StatusCode(400, new { code = 404, 
                                             error = "BAD_REQUEST PHONE_NUMBERS_INVALID", 
                                             message = "Duplicate numbers detected" });
            }

            var translitedMessage = _translitService.Translit(message);

            // Проверка сообщения
            if (String.IsNullOrEmpty(translitedMessage))
            {
                return StatusCode(405, new { code = 401, 
                                             error = "BAD_REQUEST MESSAGE_EMPTY", 
                                             message = "Invite message is missing" });
            }

            if (!_smsMessageValidator.IsAllowedSymbols(translitedMessage))
            {
                return StatusCode(400, new
                {
                    code = 406,
                    error = "BAD_REQUEST MESSAGE INVALID",
                    message = "Invite message should contain only characters in 7 bit GSM encoding or Cyrillic letters as well"
                });
            }

            if (!_smsMessageValidator.IsAllowedLength(translitedMessage))
            {
                return StatusCode(400, new
                {
                    code = 407,
                    error = "BAD_REQUEST MESSAGE INVALID",
                    message = "Invite message too long, should be less or equal to 128 characters of 7 bit GSM charsets"
                });
            }            

            //Проверка на кол-во сообщений в день
            var countTodayInvite = await _inviteRepository.GetTodayInvites(apiId);
            if (countTodayInvite + phoneList.Length > _settings.MaxCountInviteInDay)
            {
                return StatusCode(400, new { code = 403, 
                                             error = "BAD_ REQUEST PHONE_NUMBERS_INVALID", 
                                             message = "Too much phone numbers, should be less or equal to 128 per day" });
            }

            //Проверка номеров по базе
            var sendedPhones = await _inviteRepository.GetInvitesSendedByPhonesCount(phoneList);
            if (sendedPhones > 0)
            {
                return StatusCode(400, new
                {
                    code = 404,
                    error = "BAD_REQUEST PHONE_NUMBERS_INVALID",
                    message = "Duplicate numbers detected"
                });
            }

            // Отправка
            try
            {
                await _smsService.Send(translitedMessage, phoneList);
                //Сохранение в бд
                var today = _dateTimeService.GetCurrentDate();
                await _inviteRepository.SaveInviteRange(phoneList.Select(r => new Invite { ApiId = apiId, MessageText = translitedMessage, Phone = r, EventDate = today }));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Phones : {phones}, Message {message}");
                return StatusCode(500, new
                {
                    code = 500,
                    error = "BAD_REQUEST PHONE_NUMBERS_INVALID",
                    message = ex.Message
                });
            }

            return Ok();
        }
    }
}
