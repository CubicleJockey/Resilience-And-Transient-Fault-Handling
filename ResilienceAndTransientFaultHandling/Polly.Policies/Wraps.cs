using System;
using Polly.Wrap;

namespace Polly.Policies
{
    /// <summary>
    /// https://github.com/App-vNext/Polly/wiki/PolicyWrap
    /// </summary>
    public class Wraps
    {
        public PolicyWrap WrapRetryAndBreak()
        {
            var retry = Policy.Handle<Exception>()
                              .Retry(16);
            var breaker = Policy.Handle<Exception>()
                                .CircuitBreaker(4, TimeSpan.FromSeconds(4));
    
            var policyWrap = Policy.Wrap(retry, breaker);

            return policyWrap;
        }
    }
}
