using System;

namespace ConsoleApp1
{
    public struct Max<T> : IMonoid<T> where T : IComparable<T>, new()
    {
        public T Zero => new();

        public T Plus(T left, T right) =>
            left.CompareTo(right) > 0
                ? left
                : right;
    }
}