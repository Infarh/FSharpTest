namespace System.Collections.Generic
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> AddLast<T>(this IEnumerable<T> items, T Item)
        {
            foreach (var item in items) yield return item;
            yield return Item;
        }

        public static IEnumerable<T> AddLast<T>(this IEnumerable<T> items, IEnumerable<T> Items)
        {
            foreach (var item in items) yield return item;
            foreach (var item in Items) yield return item;
        }

        public static IEnumerable<T> AddLast<T>(this IEnumerable<T> items, params T[] Items)
        {
            foreach (var item in items) yield return item;
            foreach (var item in Items) yield return item;
        }

        public static IEnumerable<T> AddFirst<T>(this IEnumerable<T> items, T Item)
        {
            yield return Item;
            foreach (var item in items) yield return item;
        }

        public static IEnumerable<T> AddFirst<T>(this IEnumerable<T> items, IEnumerable<T> Items)
        {
            foreach (var item in Items) yield return item;
            foreach (var item in items) yield return item;
        }

        public static IEnumerable<T> AddFirst<T>(this IEnumerable<T> items, params T[] Items) =>
            AddFirst(items, (IEnumerable<T>)Items);
    }
}
