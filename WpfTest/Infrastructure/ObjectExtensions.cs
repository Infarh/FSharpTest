using System;

namespace System
{
    internal static class ObjectExtensions
    {
        public static T IfNotNull<T>(this T obj, Action<T> action)
        {
            if (obj != null)
                action(obj);
            return obj;
        }
    }
}
