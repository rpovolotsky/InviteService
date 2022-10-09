using Microsoft.VisualStudio.TestTools.UnitTesting;
using InviteService.Services;
using System;
using System.Collections.Generic;
using System.Text;
using InviteServiceTests;

namespace InviteService.Services.Tests
{
    [TestClass()]
    public class PhoneValidatorTests
    {
        [TestMethod()]
        public void IsValidTest()
        {
            IPhoneValidator phoneValidator = new PhoneValidator(new SettingsTests());
            Assert.IsFalse(phoneValidator.IsValid("9057779988"));            
            Assert.IsFalse(phoneValidator.IsValid("+79057779988"));
            Assert.IsFalse(phoneValidator.IsValid("7(905)7779988"));
            Assert.IsFalse(phoneValidator.IsValid("7905777-99-88"));
            Assert.IsTrue(phoneValidator.IsValid("79057779988"));
        }
    }
}