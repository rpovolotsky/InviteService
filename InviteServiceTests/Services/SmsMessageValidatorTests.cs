using Microsoft.VisualStudio.TestTools.UnitTesting;
using InviteService.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace InviteService.Services.Tests
{
    [TestClass()]
    public class SmsMessageValidatorTests
    {
        [TestMethod()]
        public void IsAllowedSymbolsTest()
        {
            var messageValidator = new SmsMessageValidator();
            Assert.IsFalse(messageValidator.IsAllowedSymbols("Кирилические символы"));
            Assert.IsTrue(messageValidator.IsAllowedSymbols("This message is"));
        }

        [TestMethod()]
        public void IsAllowedLengthTest()
        {

            var messageValidator = new SmsMessageValidator();
            Assert.IsTrue(messageValidator.IsAllowedLength("This article steps you through creating running and customizing a series of unit tests using the Microsoft unit test framework for managed code"));
            Assert.IsFalse(messageValidator.IsAllowedLength("This article steps you through creating, running, and customizing a series of unit tests using the Microsoft unit test framework for managed code"));
        }
    }
}