using System;

namespace ConsoleApp1
{
    public struct Any<T> : IMonoid<Predicate<T>>
    {
        public Predicate<T> Zero => _ => false;

        public Predicate<T> Plus(Predicate<T> left, Predicate<T> right) =>
            x => left(x) || right(x);
    }
}