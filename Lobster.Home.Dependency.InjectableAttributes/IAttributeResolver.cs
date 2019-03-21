using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lobster.Home.Dependency.InjectableAttributes
{
    public interface IAttributeResolver
    {
        IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(MemberInfo mi)
            where TAttribute : Attribute;
        TAttribute GetCustomAttribute<TAttribute>(MemberInfo mi)
            where TAttribute : Attribute;
    }
}
