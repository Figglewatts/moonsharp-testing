using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moonsharp_testing
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class LuaProxyAttribute : Attribute
    {
        public Type ProxyFactoryType { get; set; }

        public LuaProxyAttribute(Type proxyFactoryType)
        {
            ProxyFactoryType = proxyFactoryType;
        }
    }
}
