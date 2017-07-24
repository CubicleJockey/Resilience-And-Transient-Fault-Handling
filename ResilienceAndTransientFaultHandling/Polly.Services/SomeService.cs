using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Polly.Policies;

namespace Polly.Services
{
    public class SomeService
    {
        private readonly Retries retryPolicies;
        private readonly CircuitBreakers circuitBreakers;
        private readonly Timeouts timeouts;
        private readonly FallBacks fallbacks;
        private readonly Wraps wraps;

        public string BaseUrl { get; }

        public SomeService(string baseUrl)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
            {
                throw new ArgumentException($"{nameof(baseUrl)} cannot be empty.", nameof(baseUrl));
            }
            BaseUrl = baseUrl;

            retryPolicies = new Retries();
            circuitBreakers = new CircuitBreakers();
            timeouts = new Timeouts();
            fallbacks = new FallBacks();
            wraps = new Wraps();
        }

        public IEnumerable<string> DoSomeWork()
        {
            var timesTried = 0;

            IList<string> result = new List<string>();

            var retry3Policy = retryPolicies.Retry3Times();

            retry3Policy.Execute(() =>
                {
                    if (timesTried >= 2)
                    {
                        return;
                    }
                    timesTried++;
                    throw new TimeoutException();
                },
                contextData: new Dictionary<string, object>
                {
                    {nameof(result), result}
                }
            );
            return result;
        }

        public IEnumerable<string> DoMoreWork()
        {
            IList<string> AttemptLog = new List<string>();

            var retryAndWaitPolicy = retryPolicies.Retry4TimesAndWaitExponentially();

            var tryAttempts = 0;
            retryAndWaitPolicy.Execute(
                () =>
                {
                    if (tryAttempts++ < 3)
                    {
                        throw new DivideByZeroException();
                    }
                },
                new Dictionary<string, object> {{nameof(AttemptLog), AttemptLog}});


            return AttemptLog;
        }

        public void DoTheBreakAndWait(bool throwException = true)
        {
            var breakAndWaitPolicy = circuitBreakers.BreakAndWait();

            for (var i = 0; i < 3; i++)
            {
                try
                {
                    breakAndWaitPolicy.Execute(() =>
                    {
                        if (throwException)
                        {
                            throw new DivideByZeroException();
                        }
                    });
                }
                catch (DivideByZeroException)
                {
                    continue;
                }
            }
        }

        public void ChainedPolicies()
        {
            var wrap = wraps.WrapRetryAndBreak();
            wrap.Execute(() => throw new Exception("Oh noes n' stuff!"));
        }

        public void LongRunningThingy()
        {
            var timeoutPolicy = timeouts.TwoSecondTimeout();
            timeoutPolicy.Execute(() =>
            {
                Thread.Sleep(100000);
            });
        }
    }
}
