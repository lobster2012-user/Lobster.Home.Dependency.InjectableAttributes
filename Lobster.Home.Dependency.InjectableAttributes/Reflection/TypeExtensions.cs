using System;
using System.Collections.Generic;
using System.Text;

namespace Lobster.Home.Dependency.InjectableAttributes.Reflection
{
    public static class TypeExtensions
    {
        public static bool IsOf(this object o, Type baseClass)
        {
            var type = o.GetType();
            return type.IsSubclassOf(baseClass) || type == baseClass;
        }
    }
}
