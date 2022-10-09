using InviteService;
using System;
using System.Collections.Generic;
using System.Text;

namespace InviteServiceTests
{
    internal class SettingsTests : ISettings
    {
        public int MaxCountInviteSend => 3;

        public int MaxCountInviteInDay => 10;

        public string PhoneTemplate => "^7[0-9]{10}$";
    }
}
