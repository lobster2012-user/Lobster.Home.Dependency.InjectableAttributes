using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Lobster.Home.Dependency.InjectableAttributes
{
    public static class CustomAttributeResolverExtensions
    {
        public static IEnumerable<T> GetAttributes<T>(
            this ICustomAttributeResolver attributeResolver,
            ICustomAttributeProvider attributeProvider,
            bool inherit = false)
            where T : Attribute
        {
            return attributeResolver.GetCustomAttributes(attributeProvider, typeof(T), inherit).Cast<T>();
        }
        public static bool IsAttributeDefined<T>(
            this ICustomAttributeResolver attributeResolver,
            ICustomAttributeProvider attributeProvider,
            bool inherit = false)
            where T : Attribute
        {
            return attributeResolver.IsDefined(attributeProvider, typeof(T), inherit);
        }
    }
}
