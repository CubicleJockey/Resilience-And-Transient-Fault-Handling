using System;
using static System.Console;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Polly.Services
{
    public class SomeService
    {
        public string BaseUrl { get; }

        public SomeService(string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException($"{nameof(baseUrl)} cannot be empty.", nameof(baseUrl));
            }
            BaseUrl = baseUrl;
        }

        public IEnumerable<string> Retry3Times()
        {
            var timesTried = 0;

            IList<string> result = new List<string>();
            var policy = Policy.Handle<TimeoutException>()
                .Retry(3, (exception, retryCount, context) =>
                {
                   timesTried++;
                   result.Add($"Retry #{retryCount} with Exception:[{exception.Message}] on method [{nameof(Retry3Times)}]");
                });

            policy.Execute(() =>
            {
                
                if (timesTried < 2)
                {
                    throw new TimeoutException();
                }
            } );
            return result;
        }
    }
}
