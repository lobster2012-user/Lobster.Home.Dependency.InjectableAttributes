using System;
using System.Collections.Generic;
using System.Text;

namespace Lobster.Home.Dependency.InjectableAttributes.Reflection
{
    public static class TypeExtensions
    {
        public static bool IsObjectAssignableFromType(this object o, Type baseClass)
        {
            var type = o.GetType();
            return type.IsAssignableFrom(baseClass);
        }
    }
}
