using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lobster.Home.Dependency.InjectableAttributes
{
    public class AggregatedAttributeResolver : IAttributeResolver
    {
        public List<IAttributeResolver> Resolvers { get; private set; }
         = new List<IAttributeResolver>();

        public AggregatedAttributeResolver()
        {

        }

        public AggregatedAttributeResolver(params IAttributeResolver[] resolvers)
        {
            Resolvers.AddRange(resolvers ?? throw new ArgumentNullException(nameof(resolvers)));
        }

        public TAttribute GetCustomAttribute<TAttribute>(MemberInfo mi) where TAttribute : Attribute
        {
            for (var i = Resolvers.Count - 1; i >= 0; --i)
            {
                var attr = Resolvers[i].GetCustomAttribute<TAttribute>(mi);
                if (attr != null)
                    return attr;
            }
            return null;
        }

        public IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(MemberInfo mi) where TAttribute : Attribute
        {
            var total = new List<TAttribute>();
            for (var i = Resolvers.Count - 1; i >= 0; --i)
            {
                var result = Resolvers[i].GetCustomAttributes<TAttribute>(mi);
                total.AddRange(result);
                if (result is BreakableEnumerable<TAttribute>)
                {
                    break;
                }
            }
            return total.ToArray();
        }
    }
}
