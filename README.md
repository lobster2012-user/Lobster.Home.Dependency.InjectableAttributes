# Lobster.Home.Dependency.InjectableAttributes


```csharp

    public interface IAttributeResolver
    {
        IEnumerable<TAttribute> GetCustomAttributes<TAttribute>(MemberInfo mi)
            where TAttribute : Attribute;
        TAttribute GetCustomAttribute<TAttribute>(MemberInfo mi)
            where TAttribute : Attribute;
    }

```

What gives us this interface.

1. ***We can redefine built-in attributes***. An update of the code is required, which retrieves the attributes.
Мы можем использовать аттрибуты, как и раньше, но приэтом будем иметь возможность их переопределять

2. ***We can use attributes as (default) options and redefine these attributes***. Requires revision.
https://github.com/aspnet/Extensions/tree/master/src/Options
Аттрибутами можно задавать настройки, при этом можно будет эти аттрибуы переопределять.

3. Using the cache for ordinary attributes, you can make them changeable on the fly.(not implemented)
Используя кэш для обычных аттрибутов можно сделать их изменямыми на лету. В коде сейчас это не реализовано.

4. The idea is not new, the question of its implementation.


Приветствуются обсуждения. Желательно на английском или русском языках. 
Discussions are welcome. (Desirable in English or Russian)
