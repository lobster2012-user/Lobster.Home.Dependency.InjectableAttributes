using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lobster.Home.Dependency.InjectableAttributes
{
    public class AggregatedAttributeResolver : ICustomAttributeResolver
    {
        public List<ICustomAttributeResolver> Resolvers { get; private set; }
         = new List<ICustomAttributeResolver>();

        public AggregatedAttributeResolver()
        {

        }

        public AggregatedAttributeResolver(params ICustomAttributeResolver[] resolvers)
        {
            Resolvers.AddRange(resolvers ?? throw new ArgumentNullException(nameof(resolvers)));
        }

        public IEnumerable<Attribute> GetCustomAttributes(ICustomAttributeProvider provider, bool inherit)
        {
            for (var i = Resolvers.Count - 1; i >= 0; --i)
            {
                var attrs = Resolvers[i].GetCustomAttributes(provider, inherit);
                foreach (var attr in attrs)
                {
                    yield return attr;
                }
            }
        }

        public IEnumerable<Attribute> GetCustomAttributes(ICustomAttributeProvider provider, Type attributeType, bool inherit)
        {
            for (var i = Resolvers.Count - 1; i >= 0; --i)
            {
                var attrs = Resolvers[i].GetCustomAttributes(provider, attributeType, inherit);
                foreach (var attr in attrs)
                {
                    yield return attr;
                }
            }
        }

        public bool IsDefined(ICustomAttributeProvider provider, Type attributeType, bool inherit)
        {
            for (var i = Resolvers.Count - 1; i >= 0; --i)
            {
                var result = Resolvers[i].IsDefined(provider, attributeType, inherit);
                if (result)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
