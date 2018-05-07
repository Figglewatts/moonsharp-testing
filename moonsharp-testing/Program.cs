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
            

            engine.LoadAndExecute("test.lua");


            Console.ReadLine();
        }
    }
}
