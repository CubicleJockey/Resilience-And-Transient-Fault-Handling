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
            var policy = Policy.Handle<>()
        }
    }
}
