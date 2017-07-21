using Polly.Timeout;

namespace Polly.Policies
{
    /// <summary>
    /// https://github.com/App-vNext/Polly/wiki/Timeout
    /// </summary>
    public class Timeouts
    {
        public TimeoutPolicy FiveSecondTimeout()
        {
            const int SECONDS = 5;
            var policy = Policy.Timeout(SECONDS, TimeoutStrategy.Pessimistic);
            return policy;
        }
    }
}
