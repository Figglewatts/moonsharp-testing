using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoonSharp.Interpreter;

namespace moonsharp_testing
{
    [MoonSharpUserData]
    public class StateClass
    {
        public int Counter { get; private set; }

        public event EventHandler CounterIncremented;

        public int IncrementCounter(int number)
        {
            Counter += number;
            CounterIncremented?.Invoke(null, EventArgs.Empty);
            return Counter;
        }
    }
}
