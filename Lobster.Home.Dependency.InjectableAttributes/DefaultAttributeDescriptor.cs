using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lobster.Home.Dependency.InjectableAttributes
{
    public class DefaultAttributeDescriptor : ICustomAttributeDescriptor
    {
        public static ICustomAttributeDescriptor Instance
             = new DefaultAttributeDescriptor();

        public IEnumerable<Attribute> GetCustomAttributes(ICustomAttributeProvider provider, bool inherit)
        {
            if (provider is MemberInfo memberInfo)
            {
                return Attribute.GetCustomAttributes(memberInfo, inherit);
            }
            if (provider is ParameterInfo parameterInfo)
            {
                return Attribute.GetCustomAttributes(parameterInfo, inherit);
            }
            if (provider is Assembly assembly)
            {
                return Attribute.GetCustomAttributes(assembly, inherit);
            }
            if (provider is Module module)
            {
                return Attribute.GetCustomAttributes(module, inherit);
            }
            return provider.GetCustomAttributes(inherit).Cast<Attribute>();
        }

        public IEnumerable<Attribute> GetCustomAttributes(ICustomAttributeProvider provider, Type attributeType, bool inherit)
        {
            if (provider is MemberInfo memberInfo)
            {
                return Attribute.GetCustomAttributes(memberInfo, attributeType, inherit);
            }
            if (provider is ParameterInfo parameterInfo)
            {
                return Attribute.GetCustomAttributes(parameterInfo, attributeType, inherit);
            }
            if (provider is Assembly assembly)
            {
                return Attribute.GetCustomAttributes(assembly, attributeType, inherit);
            }
            if (provider is Module module)
            {
                return Attribute.GetCustomAttributes(module, attributeType, inherit);
            }
            return provider.GetCustomAttributes(attributeType, inherit).Cast<Attribute>();
        }

        public bool IsDefined(ICustomAttributeProvider provider, Type attributeType, bool inherit)
        {
            if (provider is MemberInfo memberInfo)
            {
                return Attribute.IsDefined(memberInfo, attributeType, inherit);
            }
            if (provider is ParameterInfo parameterInfo)
            {
                return Attribute.IsDefined(parameterInfo, attributeType, inherit);
            }
            if (provider is Assembly assembly)
            {
                return Attribute.IsDefined(assembly, attributeType, inherit);
            }
            if (provider is Module module)
            {
                return Attribute.IsDefined(module, attributeType, inherit);
            }
            return provider.IsDefined(attributeType, inherit);
        }
    }
}
