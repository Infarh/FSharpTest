using System;
using System.Collections;
using System.Collections.Generic;

namespace AlgorithmsLib
{
    public static class FList
    {
        public static FList<T> Empty<T>() => FList<T>.Empty;
        public static FList<T> New<T>(T item) => FList<T>.New(item, FList<T>.Empty);
        public static FList<T> New<T>(params T[] items) => New((IEnumerable<T>)items);
        public static FList<T> New<T>(IEnumerable<T> items) => FList<T>.New(items);
    }

    public class FList<T> : IEnumerable<T>
    {
        public static FList<T> Empty { get; } = new FList<T>();

        public bool IsEmpty => ReferenceEquals(this, Empty);

        public T Head { get; }
        public FList<T> Tail { get; }

        private FList()
        {
            if (Empty is not null)
                throw new InvalidOperationException("Нельзя создать ещё один пустой список. Используйте статическое свойство Empty");
        }

        private FList(T Head, FList<T> Tail)
        {
            this.Head = Head;
            this.Tail = Tail;
        }

        public static FList<T> New(T Head, FList<T> Tail) =>
            Tail.IsEmpty
                ? new FList<T>(Head, Empty)
                : new FList<T>(Head, Tail);

        public static FList<T> New(IEnumerable<T> Items)
        {
            var stack = new Stack<T>(Items);
            if (stack.Count == 0) return Empty;
            var list = Empty;
            while (stack.Count > 0)
                list = list.AddFirst(stack.Pop());
            return list;
        }

        public static FList<T> NewRecursive(IEnumerable<T> Items)
        {
            using var enumerator = Items.GetEnumerator();
            return NewRecursive(enumerator);
        }

        private static FList<T> NewRecursive(IEnumerator<T> Enumerator)
        {
            if (!Enumerator.MoveNext())
                return Empty;

            var head = Enumerator.Current;
            var tail = NewRecursive(Enumerator);
            return New(head, tail);
        }

        public FList<T> AddFirst(T Item) => New(Item, this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator()
        {
            var node = this;
            while (!node.IsEmpty)
            {
                yield return node.Head;
                node = node.Tail;
            }
        }

        public override string ToString() => $"[{string.Join("; ", this)}]";
    }
}
