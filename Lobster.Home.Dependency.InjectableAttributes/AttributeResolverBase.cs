using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lobster.Home.Dependency.InjectableAttributes
{
    public abstract class AttributeResolverBase : IAttributeResolver
    {
        public AttributeResolverBase()
        {
        }

        public TAttribute GetCustomAttribute<TAttribute>(MemberInfo mi) where TAttribute : Attribute
        {
            return this.GetCustomAttributes<TAttribute>(mi).FirstOrDefault();
        }

        public abstract IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(MemberInfo mi) 
            where TAttribute : Attribute;

    }
}
