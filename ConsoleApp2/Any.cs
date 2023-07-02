namespace ConsoleApp2
{
    public class Any<T> : IMonoid<Any<T>>
    {
        private readonly Predicate<T> _predicate;

        private Any(Predicate<T> predicate) =>
            _predicate = predicate;

        public bool Test(T obj) =>
            _predicate(obj);

        public static Any<T> operator +(Any<T> left, Any<T> right) =>
            (Predicate<T>)(x => left._predicate(x) || right._predicate(x));

        public static Any<T> AdditiveIdentity =>
            (Predicate<T>)(_ => false);

        public static implicit operator Any<T>(Predicate<T> pred) =>
            new(pred);
    }
}