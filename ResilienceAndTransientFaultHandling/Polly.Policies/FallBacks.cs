using System;
using static System.Console;
using Polly.Fallback;

namespace Polly.Policies
{
    public class FallBacks
    {
        public FallbackPolicy FallBack<TException>() where TException : Exception
        {
            var policy = Policy.Handle<TException>()
                               .Fallback(RollBack<TException>);
            return policy;
        }

        #region Helper Methods

        private static void RollBack<TException>()
        {
            WriteLine($"Rolling back because of exception {typeof(TException).Name}.");
        }

        #endregion Helper Methods
    }
}