using System;
using System.Threading.Tasks;
using Polly.Timeout;
using static System.Console;

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
                WriteLine($"Pretend Logging - Execution {context.ExecutionKey} timeout in {time.TotalSeconds} seconds.");
                //throw new TimeoutException($"Oh noes we timed out and such. After {time.TotalSeconds}.");
            }

            var policy = Policy.Timeout(2, TimeoutStrategy.Pessimistic, OnTimeout);
            return policy;
        }
    }
}
