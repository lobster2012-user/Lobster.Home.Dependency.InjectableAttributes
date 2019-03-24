using Lobster.Home.Dependency.InjectableAttributes.Collections;
using Lobster.Home.Dependency.InjectableAttributes.Reflection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Lobster.Home.Dependency.InjectableAttributes
{
    /// <summary>
    /// EXAMPLE
    /// IDEAL IMPLEMENTATION: custom + default_cache
    ///             FEATURES: add/update/remove/clear
    /// </summary>
    public class CustomAttributeResolverThreadUnsafe : ICustomAttributeDescriptor
    {
        private Attribute[] EmptyAttributeArray = Array.Empty<Attribute>();
        private Dictionary<ICustomAttributeProvider, List<Attribute>>
            _attributes = new Dictionary<ICustomAttributeProvider, List<Attribute>>();
        /*
       public bool BreakIfNotNull { get; set; }

       private List<Attribute> Get<TAttribute>(MemberInfo mi)
       {
           return Get(mi, typeof(TAttribute));
       }

       private List<Attribute> Get(MemberInfo mi, Type attribute)
       {
           if (!_attributes.TryGetValue(mi, out var dict)) return null;
           return dict.GetValueOrDefault(attribute);
       }

       private List<Attribute> GetOrAdd<TAttribute>(MemberInfo mi)
       {
           return _attributes.GetOrAdd(mi).GetOrAdd(typeof(TAttribute));
       }

       private void AssertNotNull<T>(T src, string name)
       {
           if (src == null) { throw new ArgumentNullException(name); }
       }

       public override IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(MemberInfo mi)
       {
           var list = Get<TAttribute>(mi);
           if (list == null)
           {
               return Array.Empty<TAttribute>();
           }
           var typedList = list.Cast<TAttribute>();
           if (BreakIfNotNull)
           {
               return new BreakableEnumerable<TAttribute>(typedList);
           }
           return typedList;
       }

       public CustomAttributeResolverThreadUnsafe Add<TType, TAttribute>(TAttribute attribute)
            where TAttribute : Attribute
       {
           return Add<TAttribute>(typeof(TType), attribute);
       }

       public CustomAttributeResolverThreadUnsafe Add<TAttribute>(MemberInfo mi, TAttribute attribute)
           where TAttribute : Attribute
       {
           AssertNotNull(mi, nameof(mi));
           AssertNotNull(attribute, nameof(attribute));

           GetOrAdd<TAttribute>(mi).Add(attribute);
           return this;
       }

       public CustomAttributeResolverThreadUnsafe Clear<TAttribute>(MemberInfo mi)
          where TAttribute : Attribute
       {
           return Clear(mi, typeof(TAttribute));
       }

       public CustomAttributeResolverThreadUnsafe Clear(MemberInfo mi, Type attribute)
       {
           AssertNotNull(mi, nameof(mi));
           AssertNotNull(attribute, nameof(attribute));
           var list = Get(mi, attribute);
           if (list != null)
           {
               list.Clear();
           }
           return this;
       }

       public CustomAttributeResolverThreadUnsafe AddOrReplace<TAttribute>(MemberInfo mi, TAttribute attribute)
           where TAttribute : Attribute
       {
           AssertNotNull(mi, nameof(mi));
           AssertNotNull(attribute, nameof(attribute));

           var list = GetOrAdd<TAttribute>(mi);
           list.Clear();
           list.Add(attribute);

           return this;
       }
       */

        private void AssertNotNull(Type attributeType)
        {
            if (attributeType == null)
            {
                throw new ArgumentNullException(nameof(attributeType));
            }
        }
        private void AssertIsAssignableFromAttribute(Type attributeType)
        {
            AssertNotNull(attributeType);
            if (!attributeType.IsAssignableFrom(typeof(Attribute)))
            {
                throw new ArgumentException(nameof(attributeType));
            }
        }
        private void AssertNotInherit(bool inherit)
        {
            if (inherit)
            {
                throw new NotImplementedException(nameof(inherit));
            }
        }
        public static bool IsInheritableAttribute(Type attributeType)
        {
            var usageAttr = DefaultAttributeDescriptor.Instance
                                .GetCustomAttributes<AttributeUsageAttribute>(attributeType)
                                .SingleOrDefault();
            return usageAttr?.Inherited != false;
        }

        private IEnumerable<Attribute> GetCustomAttributesPerConcreteProvider(ICustomAttributeProvider provider)
        {
            if (_attributes.TryGetValue(provider, out var list))
            {
                return list;
            }
            return Array.Empty<Attribute>();
        }
        private IEnumerable<Attribute> GetCustomAttributesPerConcreteProvider(ICustomAttributeProvider provider, Type attributeType)
        {
            for (var currentType = attributeType; currentType != null; currentType = currentType.BaseType)
            {
                if (_attributes.TryGetValue(provider, out var list))
                {
                    foreach (var attr in list.Where(z => z.IsObjectAssignableFromType(attributeType)))
                    {
                        yield return attr;
                    }
                }
            }
        }
        private IEnumerable<Attribute> GetCustomAttributesPerConcreteAttribute(ICustomAttributeProvider provider, Type attributeType)
        {
            if (_attributes.TryGetValue(provider, out var list))
            {
                return list.Where(z => z.IsObjectAssignableFromType(attributeType));
            }
            return Array.Empty<Attribute>();
        }

        public IEnumerable<Attribute> GetCustomAttributes(ICustomAttributeProvider provider, bool inherit)
        {
            AssertNotInherit(inherit);
            return GetCustomAttributesPerConcreteProvider(provider);
        }
        public IEnumerable<Attribute> GetCustomAttributes(ICustomAttributeProvider provider, Type attributeType, bool inherit)
        {
            AssertIsAssignableFromAttribute(attributeType);
            AssertNotInherit(inherit);
            return GetCustomAttributesPerConcreteProvider(provider, attributeType);
        }

        public bool IsDefined(ICustomAttributeProvider provider, Type attributeType, bool inherit)
        {
            AssertIsAssignableFromAttribute(attributeType);
            AssertNotInherit(inherit);
            if (!_attributes.TryGetValue(provider, out var list)) return false;
            return list.Any(z => z.IsObjectAssignableFromType(attributeType));
        }
    }
}
