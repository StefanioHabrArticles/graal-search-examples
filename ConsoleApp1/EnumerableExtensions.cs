using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public static class EnumerableExtensions
    {
        public static T Sum<T, M>(this IEnumerable<T> collection, M monoid = default)
            where M : struct, IMonoid<T> =>
            collection.Aggregate(monoid.Zero, monoid.Plus);
    }
}