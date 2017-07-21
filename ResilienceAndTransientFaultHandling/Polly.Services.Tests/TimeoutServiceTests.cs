using Microsoft.VisualStudio.TestTools.UnitTesting;
using Polly.Timeout;

namespace Polly.Services.Tests
{
    [TestClass]
    public class TimeoutServiceTests : TestBase
    {
        [TestMethod]
        public void FiveSecondTimeouts()
        {

            try
            {
                service.TakingTooLongOrSomething();
            }
            catch (TimeoutRejectedException trx)
            {
                Assert.AreEqual("The delegate executed through TimeoutPolicy did not complete within the timeout.", trx.Message);
                return;
            }
            Assert.Fail($"Should have gotten a {nameof(TimeoutRejectedException)}");
        }
    }
}
