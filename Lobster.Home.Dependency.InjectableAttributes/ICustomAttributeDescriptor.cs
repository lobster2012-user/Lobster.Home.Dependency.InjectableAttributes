using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Lobster.Home.Dependency.InjectableAttributes
{
    public interface ICustomAttributeDescriptor
    {
        IEnumerable<Attribute> GetCustomAttributes(ICustomAttributeProvider provider, bool inherit);
        IEnumerable<Attribute> GetCustomAttributes(ICustomAttributeProvider provider, Type attributeType, bool inherit);
        bool IsDefined(ICustomAttributeProvider provider, Type attributeType, bool inherit);
    }
}
