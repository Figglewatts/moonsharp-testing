using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Interop;

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
            // figure out if we need to create a proxy
            var proxyAttributes = (LuaProxyAttribute[])obj.GetType().GetCustomAttributes(typeof(LuaProxyAttribute), true);
            if (proxyAttributes.Length > 0)
            {
                var proxyAttr = proxyAttributes[0];
                var proxyFactory = Activator.CreateInstance(proxyAttr.ProxyFactoryType);
                UserData.RegisterProxyType(proxyFactory as IProxyFactory);
            }

            _env.Globals[obj.GetType().Name] = obj;
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
