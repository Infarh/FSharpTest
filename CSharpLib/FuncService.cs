using System;
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
            var cache = new Dictionary<T, TResult>();
            return arg => GetOrAdd(cache, arg, func);
        }

        private static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, Func<TKey, TValue> func)
        {
            if (dict.TryGetValue(key, out var value))
                return value;
            value = func(key);
            dict.Add(key, value);
            return value;
        }
    }
}
