# Lobster.Home.Dependency.InjectableAttributes
ICustomAttributeDescriptor: ***OVERRIDES BUILT-IN ATTRIBUTES***

Idea without implementation.

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
You can also bypass the limitations when setting values for attributes now. (const expressions, strings) 
It is easy to use someone else's code by redefining any attributes many times.

Огромное количество реализаций чего-либо работают с аттрибутами, при этом позволяют 
задавать настройки и без них. Получая аттрибуты через ICustomAttributeDescriptor
вы больше не привязаны жестко к жестко прописанным аттрибутам, 
вы можете поменять их на лету, удалить, добавить.
Внешне это будет единый подход к работае с аттрибутами.
И это будет работать из коробки.
Появляется возможность обойти ограничения в установки значений аттрибутов.
Можно легко использовать чужой код множетсов раз переопределяя аттрибуты.

Disadvantages.
Metadata analyzers will not work correctly.

Недостатки.
Анализаторы метаданных работать будут некорректно.

```csharp

public class OptionsAttribute : Attribute
{
     public string SomeString {get;set;}
}
public class Options
{
     public string SomeString {get;set;}
     [DefaultValue("defaultValue")]
     public string SomeString2 {get;set;}
}

[Options(SomeString="abc")]
public class SomeClass
{
    public class SomeClass(IOptions<Options> options)
    {
        ....
    }
}

...
void Initialize()
{
   var descriptor = AttributeDescriptor.Default;
   descriptor.AddCustomAttribute(typeof(CustomType), new CustomAttribute {});
   
   var options = descriptor.GetCustomAttribute<OptionsAttribute>(typeof(CustomType));
   options.SomeString = "qwerty";
   
   descriptor.AddOrUpdate<DefaultValueAttribute>(Property<Options>.Of(()=>SomeString2), new DefaultValueAttribute("tyuio"));
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
