﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InviteService.Services
{
    public interface IPhoneValidator
    {
        bool IsValid(string phone);
    }
}
