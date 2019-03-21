using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lobster.Home.Dependency.InjectableAttributes
{
    public class DefaultAttributeResolver : IAttributeResolver
    {
        public TAttribute GetCustomAttribute<TAttribute>(MemberInfo mi) where TAttribute : Attribute
        {
            return mi.GetCustomAttribute<TAttribute>();
        }

        public IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(MemberInfo mi) where TAttribute : Attribute
        {
            return mi.GetCustomAttributes<TAttribute>();
        }
    }
}
