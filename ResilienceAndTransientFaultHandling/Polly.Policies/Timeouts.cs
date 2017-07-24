using System;
using System.Threading.Tasks;
using Polly.Timeout;

namespace Polly.Policies
{
    /// <summary>
    /// https://github.com/App-vNext/Polly/wiki/Timeout
    /// </summary>
    public class Timeouts
    {
        public TimeoutPolicy TwoSecondTimeout()
        {
            void OnTimeout(Context context, TimeSpan time, Task task)
            {
                throw new TimeoutException($"Oh noes we timed out and such. After {time.TotalSeconds}.");
            }

            var policy = Policy.Timeout(2, OnTimeout);
            return policy;
        }
    }
}
