using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoonSharp.Interpreter;

namespace moonsharp_testing
{
    public class StateClass
    {
        public int Counter { get; set; }

        public StateSubClass Sub { get; }

        public StateClass()
        {
            Sub = new StateSubClass();
        }
    }
    
    public class StateSubClass
    {
        public string Value = "";
    }

    public class LuaStateClass
    {
        private StateClass _target;

        public event EventHandler CounterIncremented;

        public int IncrementCounter(int number)
        {
            _target.Counter += number;
            CounterIncremented?.Invoke(null, EventArgs.Empty);
            return _target.Counter;
        }

        public string Sub
        {
            get => _target.Sub.Value;
            set => _target.Sub.Value = value;
        }

        [MoonSharpHidden]
        public LuaStateClass(StateClass c)
        {
            _target = c;
        }
    }
}
