using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lobster.Home.Dependency.InjectableAttributes
{
    public static class CustomAttributeDescriptorExtensions
    {
        public static T GetCustomAttribute<T>(this ICustomAttributeDescriptor attributeResolver, ICustomAttributeProvider attributeProvider, bool inherit = true)
            where T : Attribute
        {
            return GetCustomAttributes<T>(attributeResolver, attributeProvider, inherit).SingleOrDefault();
        }
        public static IEnumerable<T> GetCustomAttributes<T>(this ICustomAttributeDescriptor attributeResolver, ICustomAttributeProvider attributeProvider, bool inherit = true)
            where T : Attribute
        {
            return attributeResolver.GetCustomAttributes(attributeProvider, typeof(T), inherit).Cast<T>();
        }
        public static bool IsAttributeDefined<T>(this ICustomAttributeDescriptor attributeResolver, ICustomAttributeProvider attributeProvider, bool inherit = true) where T : Attribute
        {
            return attributeResolver.IsDefined(attributeProvider, typeof(T), inherit);
        }
    }
}
