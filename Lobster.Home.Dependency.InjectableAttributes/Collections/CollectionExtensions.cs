using System;
using System.Collections.Generic;
using System.Text;

namespace Lobster.Home.Dependency.InjectableAttributes.Collections
{
    public static class CollectionExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(
               this IDictionary<TKey, TValue> dict, TKey key)
            where TValue : new()
        {
            return GetOrAdd(dict, key, _ => new TValue());
        }
        public static TValue GetOrAdd<TKey, TValue>(
            this IDictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> factory)
        {
            if (!dict.TryGetValue(key, out var result))
            {
                dict.Add(key, result = factory(key));
            }
            return result;
        }
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        {
            dict.TryGetValue(key, out var result);
            return result;
        }
    }
}
