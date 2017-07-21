using System;
using System.Collections.Generic;
using Polly.Retry;

namespace Polly.Policies
{
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
    }
}
