using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MoonSharp.Interpreter;

namespace moonsharp_testing
{
    public class LuaEngine
    {
        private Script _env;

        public LuaEngine()
        {
            createEnvironment();
            createStaticAPI();
        }

        public void LoadAndExecute(string filename)
        {
            try
            {
                _env.DoFile(filename);
            }
            catch (InternalErrorException e)
            {
                Console.WriteLine(e.ToString());
            }
            catch (SyntaxErrorException e)
            {
                Console.WriteLine("Lua Syntax Error: {0}", e.DecoratedMessage);
            }
            catch (ScriptRuntimeException e)
            {
                Console.WriteLine("Lua Script Error: {0}", e.DecoratedMessage);
            }
        }

        public void Provide(object obj)
        {
            _env.Globals[obj.GetType().Name] = obj;
        }

        public static void RegisterProxy<Proxy, Target>(Func<Target, Proxy> wrapDelegate)
            where Proxy : class
            where Target : class
        {
            UserData.RegisterProxyType(wrapDelegate);
        }

        private void createEnvironment()
        {
            UserData.RegisterAssembly();
            UserData.RegisterType<EventArgs>();
            _env = new Script();
        }

        private void createStaticAPI()
        {
            MethodInfo[] api = typeof(LuaStaticAPI).GetMethods(BindingFlags.Static | BindingFlags.Public);
            foreach (var method in api)
            {
                _env.Globals[method.Name] = method;
            }
        }
    }
}
