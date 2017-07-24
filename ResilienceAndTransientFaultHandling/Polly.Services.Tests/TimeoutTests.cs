using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Polly.Services.Tests
{
    [TestClass]
    public class TimeoutTests : TestBase
    {
        [TestMethod]
        public void Timeout()
        {
            service.LongRunningThingy();
        }
    }
}
