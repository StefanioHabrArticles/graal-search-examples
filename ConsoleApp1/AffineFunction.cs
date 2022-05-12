namespace ConsoleApp1
{
    public class AffineFunction<T, R> 
        where R : struct, IRing<T>
    {
        public T Slope { get; }
        public T Intercept { get; }

        public AffineFunction(T slope, T intercept)
        {
            Slope = slope;
            Intercept = intercept;
        }

        public T Apply(T x)
        {
            var ring = default(R);
            return ring.Plus(ring.Times(Slope, x), Intercept);
        }

        public override string ToString() =>
            $"{Slope} * X |+| {Intercept}";
    }

    public struct AffineFunctionMonoid<T, R> : IMonoid<AffineFunction<T, R>>
        where R : struct, IRing<T>
    {
        private R _ring;

        public AffineFunctionMonoid(R ring = default)
        {
            _ring = ring;
        }

        public AffineFunction<T, R> Zero => new(_ring.One, _ring.Zero);

        public AffineFunction<T, R> Plus(AffineFunction<T, R> left, AffineFunction<T, R> right)
        {
            var newSlope = _ring.Times(left.Slope, right.Slope);
            var newIntercept = right.Apply(left.Intercept);
            return new AffineFunction<T, R>(newSlope, newIntercept);
        }
    }
}