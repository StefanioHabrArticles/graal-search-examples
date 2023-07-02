using System.Numerics;

namespace ConsoleApp2
{
    public static class EnumerableExtensions
    {
        public static TMonoid Sum<TMonoid>(this IEnumerable<TMonoid> collection)
            where TMonoid :
            IAdditionOperators<TMonoid, TMonoid, TMonoid>,
            IAdditiveIdentity<TMonoid, TMonoid> =>
            collection.Aggregate(TMonoid.AdditiveIdentity, (x, y) => x + y);
    }
}