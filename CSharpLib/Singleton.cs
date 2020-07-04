using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpLib
{
    public class Singleton<T>
    {
        public readonly Lazy<T> _Lazy;
        
        public T Instance => _Lazy.Value;

        public Singleton(Func<T> Initializer) => _Lazy = new Lazy<T>(Initializer, true);
    }

    public class SingletonValue<T> : Singleton<T> where T : new()
    {
        private static readonly Singleton<T> _Value = new Singleton<T>(() => new T());

        public static T Value => _Value.Instance;

        private SingletonValue(Func<T> Initializer) : base(Initializer) { }
    }
}
