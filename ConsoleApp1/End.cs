using System;

namespace ConsoleApp1
{
    public struct End<T, G> : IRing<Func<T, T>>
        where G : struct, IGroup<T>
    {
        public Func<T, T> Plus(Func<T, T> left, Func<T, T> right) => 
            x => default(G).Plus(left(x), right(x));

        public Func<T, T> Zero => _ => default(G).Zero;

        public Func<T, T> Inverse(Func<T, T> item) =>
            x => default(G).Inverse(x);

        public Func<T, T> One => x => x;

        public Func<T, T> Times(Func<T, T> left, Func<T, T> right) =>
            x => left(right(x));
    }
}