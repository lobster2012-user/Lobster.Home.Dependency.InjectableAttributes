using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lobster.Home.Dependency.InjectableAttributes
{
    public class DefaultMetaAttributeResolver : ICustomAttributeResolver
    {
        public IEnumerable<Attribute> GetCustomAttributes(ICustomAttributeProvider provider, bool inherit)
        {
            return provider.GetCustomAttributes(inherit).Cast<Attribute>();
        }

        public IEnumerable<Attribute> GetCustomAttributes(ICustomAttributeProvider provider, Type attributeType, bool inherit)
        {
            return provider.GetCustomAttributes(attributeType, inherit).Cast<Attribute>();
        }

        public bool IsDefined(ICustomAttributeProvider provider, Type attributeType, bool inherit)
        {
            return provider.IsDefined(attributeType, inherit);
        }
    }
}
