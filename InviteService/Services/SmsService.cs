using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InviteService.Services
{
    public class SmsService : ISmsService
    {
        public async Task Send(string message, IEnumerable<string> phones)
        {
            await Task.CompletedTask;
        }
    }
}
