using System;
using System.Drawing;
using Polly;
using Console = Colorful.Console;

namespace PollyPOC.Core.Policies
{
    public static class CircuitBreakerPolicy
    {
        // This need to be persistent across all calls. A better mechanism can be used here. Setting static for now.
        public static readonly Policy circuitBreakerPolicy = GetCircuitBreakerPolicy();

        private static Policy GetCircuitBreakerPolicy()
        {
            /*
            Break the circuit after the specified number of consecutive exceptions
            and keep circuit broken for the specified duration,
            calling an action on change of circuit state.

            https://github.com/App-vNext/Polly#circuit-breaker
        */

            return Policy.Handle<Exception>()
                .CircuitBreaker(2, TimeSpan.FromSeconds(15),
                    (e, t) => { Console.WriteLine("Circuit has been broken.", Color.Red); },
                    () => { Console.WriteLine("Circuit has been reset.", Color.Yellow); });
        }
    }
}