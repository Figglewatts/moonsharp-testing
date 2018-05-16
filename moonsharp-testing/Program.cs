using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MoonSharp.Interpreter;

namespace moonsharp_testing
{
    class Program
    {
        static void Main(string[] args)
        {
            LuaEngine engine = new LuaEngine();
            
            StateClass c = new StateClass();
            
            engine.Provide(c);
            engine.LoadAndExecute("lua/test.lua");

            Console.WriteLine(c.Counter);

            

            Console.ReadLine();
        }
    }
}
