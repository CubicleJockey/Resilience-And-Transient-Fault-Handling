using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Polly.CircuitBreaker;

namespace Polly.Services.Tests
{
    [TestClass]
    public class CircuitBreakerTests : TestBase
    {


        [TestMethod]
        public void BreakAndWait_WaitTimeMet()
        {
            BreakAndWaitTest(true);
        }



        [TestMethod]
        public void BreakAndWait_WaitTimeNotMet()
        {
            BreakAndWaitTest();
        }

        #region Helper Method

        private void BreakAndWaitTest(bool waitRetryTime = false)
        {
            try
            {
                service.DoTheBreakAndWait();
            }
            catch (BrokenCircuitException bcx)
            {
                Assert.AreEqual("The circuit is now open and is not allowing calls.", bcx.Message);

                if (waitRetryTime)
                {
                    Thread.Sleep(15000); //Wait the 3 second retry limit.
                }

                const bool THROWEXCEPTION = false;

                try
                {
                    service.DoTheBreakAndWait(THROWEXCEPTION);
                }
                catch (BrokenCircuitException)
                {
                    Assert.Fail("Should have not met the broken circuit.");
                }

                return;
            }
            Assert.Fail("The Circuit should have Broken.");
        }

        #endregion Helper Method
    }
}
