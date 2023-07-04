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
            new(x => left._predicate(x) || right._predicate(x));

        public static Any<T> AdditiveIdentity =>
            new(_ => false);

        public static explicit operator Any<T>(Predicate<T> pred) =>
            new(pred);
    }
}