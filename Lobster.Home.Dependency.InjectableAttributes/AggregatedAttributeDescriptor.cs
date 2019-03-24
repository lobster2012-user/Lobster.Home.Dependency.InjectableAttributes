using System;
using System.Collections.Generic;
using System.Reflection;

namespace Lobster.Home.Dependency.InjectableAttributes
{
    public class AggregatedAttributeDescriptor : ICustomAttributeDescriptor
    {
        public List<ICustomAttributeDescriptor> Descriptors { get; private set; }
         = new List<ICustomAttributeDescriptor>();

        public AggregatedAttributeDescriptor()
        {

        }

        public AggregatedAttributeDescriptor(params ICustomAttributeDescriptor[] descriptor)
        {
            Descriptors.AddRange(descriptor ?? throw new ArgumentNullException(nameof(descriptor)));
        }

        public IEnumerable<Attribute> GetCustomAttributes(ICustomAttributeProvider provider, bool inherit)
        {
            for (var i = Descriptors.Count - 1; i >= 0; --i)
            {
                var attrs = Descriptors[i].GetCustomAttributes(provider, inherit);
                foreach (var attr in attrs)
                {
                    yield return attr;
                }
            }
        }

        public IEnumerable<Attribute> GetCustomAttributes(ICustomAttributeProvider provider, Type attributeType, bool inherit)
        {
            for (var i = Descriptors.Count - 1; i >= 0; --i)
            {
                var attrs = Descriptors[i].GetCustomAttributes(provider, attributeType, inherit);
                foreach (var attr in attrs)
                {
                    yield return attr;
                }
            }
        }

        public bool IsDefined(ICustomAttributeProvider provider, Type attributeType, bool inherit)
        {
            for (var i = Descriptors.Count - 1; i >= 0; --i)
            {
                var result = Descriptors[i].IsDefined(provider, attributeType, inherit);
                if (result)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
