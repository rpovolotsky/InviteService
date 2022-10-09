using System.Text.RegularExpressions;

namespace InviteService.Services
{
    public class PhoneValidator : IPhoneValidator
    {
        private readonly ISettings _settings;
        public PhoneValidator(ISettings settings)
        {
            _settings = settings;
        }
        public bool IsValid(string phone)
        {
            return Regex.IsMatch(phone, _settings.PhoneTemplate);
        }
    }
}
