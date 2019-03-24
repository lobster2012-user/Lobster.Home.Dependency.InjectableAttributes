# Lobster.Home.Dependency.InjectableAttributes
IAttributeDescriptor: OVERRIDES BUILT-IN ATTRIBUTES

Idea without implementation
Any comments and corrections to the translation are welcome.


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
The use of attributes will not constrain you ***out of the box***.

Огромное количество реализаций чего-либо работают с аттрибутами, при этом позволяют 
задавать настройки и без них. Получая аттрибуты через ICustomAttributeDescriptor
вы больше не привязаны жестко к жестко прописанным аттрибутам, 
вы можете поменять их на лету, удалить, добавить. И это будет работать из коробки.

```csharp

...
void Initialize()
{
   AttributeDescriptor.Default.AddAttributes(typeof(CustomType), new CustomAttribute {});
}

void Run()
{
   _someLib.Run();
}
...

class SomeLib
{
   void Initialize()
   {
     AttributeDescriptor.Default.GetCustomAttribute<CustomAttribute>(typeof(CustomType));
   }
}

```
