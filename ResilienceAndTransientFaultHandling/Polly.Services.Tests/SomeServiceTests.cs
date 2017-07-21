using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Polly.Services.Tests
{
    [TestClass]
    public class SomeServiceTests
    {
        private readonly SomeService service;

        public SomeServiceTests()
        {
            service = new SomeService("http://localhost");
        }

        [TestMethod]
        public void Retry3Times()
        {
            const int EXPECTED_RESULTS = 2;
            var result = service.Retry3Times().ToArray();

            Assert.AreEqual(EXPECTED_RESULTS, result.Length); //Third attempt succeeded
            for (var i = 0; i < EXPECTED_RESULTS;)
            {
                Assert.AreEqual($"Retry #{i + 1} with Exception:[The operation has timed out.] on method [{nameof(service.Retry3Times)}]", result[i]);
                i++;
                return;
            }
            Assert.Fail("Should be exactly be 2 results.");
        }
    }
}
