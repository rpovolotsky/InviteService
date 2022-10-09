using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InviteService.Services
{
    public interface ISmsMessageValidator
    {
        bool IsAllowedSymbols(string message);
        bool IsAllowedLength(string message);
    }
}
