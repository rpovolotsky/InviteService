using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InviteService
{
    public class Settings : ISettings
    {
        private readonly int maxCountInviteSend;
        private readonly int maxCountInviteInDay;
        private readonly string phoneTemplate;

        private IConfiguration _configuration;
        public Settings(IConfiguration configuration)
        {
            _configuration = configuration;
            maxCountInviteSend = _configuration.GetValue<int>("Settings:MaxCountInviteSend", 16);
            maxCountInviteInDay = _configuration.GetValue<int>("Settings:MaxCountInviteInDay", 128);
            phoneTemplate = _configuration.GetValue<string>("Settings:PhoneTemplate", "^7[0-9]{10}$");
        }

        public int MaxCountInviteSend => maxCountInviteSend;

        public int MaxCountInviteInDay => maxCountInviteInDay;

        public string PhoneTemplate => phoneTemplate;
    }
}
