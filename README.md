# Lobster.Home.Dependency.InjectableAttributes
IAttributeResolver: OVERRIDES BUILT-IN ATTRIBUTES


```csharp

    public interface ICustomAttributeDescriptor
    {
        IEnumerable<Attribute> GetCustomAttributes(ICustomAttributeProvider provider, bool inherit);
        IEnumerable<Attribute> GetCustomAttributes(ICustomAttributeProvider provider, Type attributeType, bool inherit);
        bool IsDefined(ICustomAttributeProvider provider, Type attributeType, bool inherit);
    }
    
    public static class CustomAttributeDescriptorExtensions
    {
        public static T GetCustomAttribute<T>(this ICustomAttributeDescriptor attributeResolver, ICustomAttributeProvider attributeProvider, bool inherit = false)
            where T : Attribute
        {
            return GetCustomAttributes<T>(attributeResolver, attributeProvider, inherit).SingleOrDefault();
        }
        public static IEnumerable<T> GetCustomAttributes<T>(this ICustomAttributeDescriptor attributeResolver, ICustomAttributeProvider attributeProvider, bool inherit = false)
            where T : Attribute
        {
            return attributeResolver.GetCustomAttributes(attributeProvider, typeof(T), inherit).Cast<T>();
        }
        public static bool IsAttributeDefined<T>(this ICustomAttributeDescriptor attributeResolver, ICustomAttributeProvider attributeProvider, bool inherit = false) where T : Attribute
        {
            return attributeResolver.IsDefined(attributeProvider, typeof(T), inherit);
        }
    }

```


A huge number of implementations of something that allows you to work with and without attributes.
Using the attribute descriptor, we have a unified approach to working with and without attributes, 
attributes can be redefined via the descriptor, if it allows it. 
You can delete attributes, change, add, referring only to the descriptor. 
The use of attributes will not constrain you from the box.


```csharp

...
AttributeDescriptor.Default.AddAttributes(typeof(CustomType), new CustomAttribute {});

...
AttributeDescriptor.Default.GetCustomAttribute<CustomAttribute>(typeof(CustomType));



```
