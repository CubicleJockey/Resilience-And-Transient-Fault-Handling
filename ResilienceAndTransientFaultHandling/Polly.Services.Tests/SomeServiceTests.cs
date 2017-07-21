using System;
using static System.Console;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Polly.Policies;

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
            var result = service.DoSomeWork().ToArray();

            Assert.AreEqual(EXPECTED_RESULTS, result.Length); //Third attempt succeeded
            for (var i = 0; i < EXPECTED_RESULTS; i++)
            {
                var expected = $"Retry #{i + 1} with Exception:[The operation has timed out.] on method [{nameof(Retries.Retry3Times)}]";
                Assert.AreEqual(expected, result[i]);
                WriteLine(expected);
            }
        }

        [TestMethod]
        public void RetryAndWaitExponentially()
        {
            const int EXPECTED_RESULTS = 3;
            var result = service.DoMoreWork().ToArray();

            Assert.AreEqual(EXPECTED_RESULTS, result.Length);

            
            for (var i = 0; i < EXPECTED_RESULTS; i++)
            {
                var expected = $"Retry Wait-Time is {TimeSpan.FromSeconds(Math.Pow(2, i + 1)).TotalSeconds} seconds.";
                Assert.AreEqual(expected, result[i]);
                WriteLine(expected);
            }
           
            
        }
    }
}
