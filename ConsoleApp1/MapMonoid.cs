using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public struct MapMonoid<K, V, S> : IMonoid<Dictionary<K, V>> 
        where S : struct, ISemiGroup<V>
    {
        public Dictionary<K, V> Zero => new();
        
        public Dictionary<K, V> Plus(Dictionary<K, V> left, Dictionary<K, V> right)
        {
            var valueSemiGroup = default(S);
            var result = Zero;
            foreach (var (key, value) in left.Concat(right))
            {
                result[key] = result.TryGetValue(key, out var v)
                    ? valueSemiGroup.Plus(v, value)
                    : value;
            }
            
            return result;
        }
    }
}