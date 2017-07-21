using System;
using System.Collections.Generic;
using System.Threading;
using Polly.Policies;

namespace Polly.Services
{
    public class SomeService
    {
        private readonly Retries retryPolicies;

        public string BaseUrl { get; }

        public SomeService(string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException($"{nameof(baseUrl)} cannot be empty.", nameof(baseUrl));
            }
            BaseUrl = baseUrl;
            retryPolicies = new Retries();
        }

        public IEnumerable<string> Retry3Times()
        {
            var timesTried = 0;

            IList<string> result = new List<string>();

            var retry3Policy = retryPolicies.Retry3Times();

            retry3Policy.Execute(() =>
                {
                    //context[nameof(result)] = result;
                    //context[nameof(timesTried)] = timesTried;
                    if (timesTried < 2)
                    {
                        timesTried++;
                        throw new TimeoutException();
                    }
                },
                contextData: new Dictionary<string, object>
                {
                    {nameof(result), result},
                    {nameof(timesTried), timesTried}
                }
                );
            return result;
        }
    }
}
