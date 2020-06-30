using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CSharpLib
{
    public static class FuncService
    {
        /// <summary>Композиция функций</summary>
        /// <typeparam name="TValue">Тип первичного значения аргумента</typeparam>
        /// <typeparam name="TArgument">Тип промежуточного значения аргумента</typeparam>
        /// <typeparam name="TResult">Тип результата</typeparam>
        /// <param name="GetArgument">Первичная функция, вычисляющая значение аргумента для вторичной функции</param>
        /// <param name="GetResult">Вторичная функция, вычисляющая итоговое значение</param>
        /// <returns>Функция композиции двух мункций</returns>
        public static Func<TValue, TResult> Compose<TValue, TArgument, TResult>(
            this Func<TValue, TArgument> GetArgument, 
            Func<TArgument, TResult> GetResult) => 
            value => GetResult(GetArgument(value));

        /// <summary>Потокобезопасная мемоизация с защитой от многократной инициализации значений кеша</summary>
        /// <typeparam name="T">Тип аргумента функции</typeparam>
        /// <typeparam name="TResult">Тип значения</typeparam>
        /// <param name="func">Мемоизируемая функция</param>
        /// <returns>Функция с мемоизацией</returns>
        public static Func<T, TResult> MemorizeThreadSafeLazy<T, TResult>(this Func<T, TResult> func)
            where T : IComparable
        {
            var cache = new ConcurrentDictionary<T, Lazy<TResult>>();
            return arg => cache.GetOrAdd(arg, v => new Lazy<TResult>(() => func(v))).Value;
        }

        /// <summary>Потокобезопасная мемоизация</summary>
        /// <typeparam name="T">Тип аргумента функции</typeparam>
        /// <typeparam name="TResult">Тип значения</typeparam>
        /// <param name="func">Мемоизируемая функция</param>
        /// <returns>Функция с мемоизацией</returns>
        public static Func<T, TResult> MemorizeThreadSafe<T, TResult>(this Func<T, TResult> func)
            where T : IComparable
        {
            var cache = new ConcurrentDictionary<T, TResult>();
            return arg => cache.GetOrAdd(arg, func);
        }

        /// <summary>Мемоизация функции</summary>
        /// <typeparam name="T">Тип аргумента функции</typeparam>
        /// <typeparam name="TResult">Тип значения</typeparam>
        /// <param name="func">Мемоизируемая функция</param>
        /// <returns>Функция с мемоизацией</returns>
        public static Func<T, TResult> Memorize<T, TResult>(this Func<T, TResult> func)
            where T : IComparable
        {
            var cache = new Dictionary<T, TResult>();
            return arg => GetOrAddValue(cache, arg, func);
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
