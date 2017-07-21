using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Polly.CircuitBreaker;

namespace Polly.Services.Tests
{
    [TestClass]
    public class CircuitBreakerServiceTests : TestBase
    {
        

        [TestMethod]
        public void BreakAndWait()
        {
            try
            {
                service.DoTheBreakAndWait();
            }
            catch (BrokenCircuitException bcx)
            {
                Assert.AreEqual("The circuit is now open and is not allowing calls.", bcx.Message);
                
                Thread.Sleep(3000); //Wait the 3 second retry limit.

                const bool THROWEXCEPTION = false;

                try
                {
                    service.DoTheBreakAndWait(THROWEXCEPTION);
                }
                catch (Exception)
                {
                    Assert.Fail("Should have exceeded.");
                }

                return;
            }
            Assert.Fail("The Circuit should have Broken.");
        }
    }
}
