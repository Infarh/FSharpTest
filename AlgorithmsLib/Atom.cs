using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AlgorithmsLib
{
    public class Atom<T> where T : class
    {
        private volatile T _Value;

        public T Value => _Value;

        public Atom(T Value) => _Value = Value;

        public T Swap(Func<T, T> Factory)
        {
            T original, tmp;
            do
            {
                original = _Value;
                tmp = Factory(original);
            }
            while (Interlocked.CompareExchange(ref _Value, tmp, original) != original);
            // Interlocked.CompareExchange(ref _Value, tmp, original)
            // выполняет попытку установки значения поля _Value только в том случае если его значение равно original
            // В любом случае возвращается значение поля до момента попытки установки его значения

            return original;
        }
    }
}
