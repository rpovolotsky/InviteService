using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InviteService.Services
{
    public interface ISmsService
    {
        Task Send(string message, IEnumerable<string> phones);
    }
}
