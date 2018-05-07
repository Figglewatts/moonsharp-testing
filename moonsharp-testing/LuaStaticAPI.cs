using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moonsharp_testing
{
    public static class LuaStaticAPI
    {
        public static void PrintHelloWorld()
        {
            Console.WriteLine("HelloWorld!");
        }

        public static void Add(int a, int b)
        {
            Console.WriteLine("{0}", a + b);
        }
    }
}
