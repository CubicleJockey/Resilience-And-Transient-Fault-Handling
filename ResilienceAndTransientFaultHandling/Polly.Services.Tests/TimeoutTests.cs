using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Console;
using Polly.Timeout;

namespace Polly.Services.Tests
{
    [TestClass]
    public class TimeoutTests : TestBase
    {
        [TestMethod]
        public void Timeout()
        {
            try
            {
                service.LongRunningThingy();
            }
            catch (TimeoutRejectedException trx)
            {
                Assert.AreEqual("The delegate executed through TimeoutPolicy did not complete within the timeout.", trx.Message);
                return;
            }
            Assert.Fail("Should have timed out.");
        }
    }
}
