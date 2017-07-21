using System;
using System.Collections.Generic;
using Polly.Retry;

namespace Polly.Policies
{
    /// <summary>
    /// https://github.com/App-vNext/Polly/wiki/Retry
    /// </summary>
    public class Retries
    {
        public RetryPolicy Retry3Times()
        {
            var policy = Policy.Handle<TimeoutException>()
                .Retry(3, (exception, retryCount, context) =>
                {
                    IList<string> result = (List<string>) context["result"];
                    result.Add($"Retry #{retryCount} with Exception:[{exception.Message}] on method [{nameof(Retry3Times)}]");
                });

            return policy;
        }

        public Policy Retry4TimesAndWaitExponentially()
        {
            var policy = Policy
                .Handle<DivideByZeroException>()
                .WaitAndRetry(4, (retryAttempt, context) =>
                    {
                        IList<string> attemptLog = (List<string>)context["AttemptLog"];
                        var retryTime = TimeSpan.FromSeconds(Math.Pow(2, retryAttempt));

                        attemptLog.Add($"Retry Wait-Time is {retryTime.TotalSeconds} seconds.");

                        return retryTime;
                    }
                );

            return policy;
        }
    }
}
