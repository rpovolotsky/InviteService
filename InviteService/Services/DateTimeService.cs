using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InviteService.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime GetCurrentDate()
        {
            return DateTime.UtcNow;
        }
    }
}
