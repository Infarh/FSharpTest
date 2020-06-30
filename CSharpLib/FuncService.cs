using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CSharpLib
{
    public static class FuncService
    {
        public static Func<TValue, TResult> Compose<TValue, TArgument, TResult>(
            this Func<TValue, TArgument> GetArgument, 
            Func<TArgument, TResult> GetResult) => 
            value => GetResult(GetArgument(value));

        public static Func<T, TResult> Memorize<T, TResult>(this Func<T, TResult> func)
            where T : IComparable
        {
            //var cache = new Dictionary<T, TResult>();
            //return arg => GetOrAddValue(cache, arg, func);
            var cache = new ConcurrentDictionary<T, TResult>();
            return arg => cache.GetOrAdd(arg, func);
        }

        private static TValue GetOrAddValue<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> func)
        {
            if (dict.TryGetValue(key, out var value))
                return value;
            value = func(key);
            dict.Add(key, value);
            return value;
        }
    }
}
