using System;
using System.IO;
using Polly.CircuitBreaker;

namespace Polly.Policies
{
    /// <summary>
    /// https://github.com/App-vNext/Polly/wiki/Circuit-Breaker
    /// </summary>
    public class CircuitBreakers
    {
        /// <summary>
        /// Break the circuit after the specified number of consecutive exceptions
        /// and keep circuit broken for the specified duration,
        /// calling an action on change of circuit state.
        /// </summary>
        /// <returns>CircuitBreakerPolicy</returns>
        public CircuitBreakerPolicy BreakAndWait()
        {
            const int EXCEPTIONS_BEFORE_BREAK = 2;
            var DURATION_OF_BREAK = TimeSpan.FromSeconds(3);

            void OnBreak(Exception exception, TimeSpan timespan)
            {
                Console.WriteLine($"Maximum exceptions of {EXCEPTIONS_BEFORE_BREAK} has been reached. Breaking code and resumes in {timespan.TotalSeconds}.");
            }

            void OnReset()
            {
                Console.WriteLine("Broken circuit is resuming.");
            }

            var policy = Policy
                .Handle<DivideByZeroException>()
                .CircuitBreaker(EXCEPTIONS_BEFORE_BREAK, DURATION_OF_BREAK, OnBreak, OnReset);

            return policy;
        }
    }
}
