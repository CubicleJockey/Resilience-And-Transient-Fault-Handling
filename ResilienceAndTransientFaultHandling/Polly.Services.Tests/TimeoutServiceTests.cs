using Microsoft.VisualStudio.TestTools.UnitTesting;
using Polly.Timeout;

namespace Polly.Services.Tests
{
    [TestClass]
    public class TimeoutServiceTests : TestBase
    {
        [TestMethod]
        public void Timeout()
        {


                service.LongRunningThingy();
            
        }
    }
}
