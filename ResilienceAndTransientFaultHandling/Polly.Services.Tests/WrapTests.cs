using Microsoft.VisualStudio.TestTools.UnitTesting;
using Polly.CircuitBreaker;

namespace Polly.Services.Tests
{
    [TestClass]
    public class WrapTests : TestBase
    {
        [TestMethod]
        public void Wraps()
        {
            try
            {
                service.ChainedPolicies();
            }
            catch (BrokenCircuitException bcx)
            {
                Assert.AreEqual("The circuit is now open and is not allowing calls.", bcx.Message);
                Assert.AreEqual("Oh noes n' stuff!", bcx.InnerException.Message);
            }
        }
    }
}
