using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InviteService.Services
{
    public class SmsMessageValidator : ISmsMessageValidator
    {
        public bool IsAllowedLength(string message)
        {
            if (Regex.IsMatch(message, "^[A-Za-z ]*$"))
            {
                return message.Length <= 160;
            }
            else
            {
                return message.Length <= 128;
            }
        }

        public bool IsAllowedSymbols(string message)
        {
            var gsmRegx = "^[A-Za-z0-9 \\r\\n@£$¥èéùìòÇØøÅå\u0394_\u03A6\u0393\u039B\u03A9\u03A0\u03A8\u03A3\u0398\u039EÆæßÉ!\"#$%&amp;'()*+,\\-./:;&lt;=&gt;?¡ÄÖÑÜ§¿äöñüà^{}\\\\\\[~\\]|\u20AC]*$";
            return Regex.IsMatch(message, gsmRegx);
        }
    }
}
