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

1. ***We can redefine attributes***. An update of the code is required, which retrieves the attributes.

2. ***We can use attributes as options***. Requires revision.
https://github.com/aspnet/Extensions/tree/master/src/Options
