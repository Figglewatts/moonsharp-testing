using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

namespace moonsharp_testing
{
    [LuaProxy(typeof(LuaStateClass))]
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

    public class StateClassProxyFactory : IProxyFactory
    { 
        public object CreateProxyObject(object o)
        {
            if (o is StateClass @class)
            {
                return new LuaStateClass(@class);
            }
            throw new ArgumentException("Object must be of type StateClass", nameof(o));
        }

        public Type TargetType => typeof(StateClass);
        public Type ProxyType => typeof(LuaStateClass);
    }

    public class LuaStateClass
    {
        private StateClass _target;

        public event EventHandler CounterIncremented;

        public int IncrementCounter(int number)
        {
            _target.Counter += number;
            CounterIncremented?.Invoke(null, EventArgs.Empty);
            Console.WriteLine("From proxy");
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
