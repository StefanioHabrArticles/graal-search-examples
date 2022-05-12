using System.Collections.Generic;

namespace ConsoleApp1
{
    public record PairedHashSet<T>(HashSet<T> First, HashSet<T> Second)
    {
        public PairedHashSet() : this(new(), new())
        {
        }

        public PairedHashSet(IEnumerable<T> single) : this(new(single), new())
        {
        }

        public PairedHashSet(T item) : this(new HashSet<T> {item})
        {
        }
    }

    public static class PairedSetExtensions
    {
        public static HashSet<T> ToHashSet<T>(this PairedHashSet<T> pairedHashSet)
        {
            var (first, second) = pairedHashSet;
            return first.Except(second);
        }
    }

    public static class HashSetExtensions
    {
        public static HashSet<T> Except<T>(this HashSet<T> first, HashSet<T> second)
        {
            var result = new HashSet<T>(first);
            result.ExceptWith(second);
            return result;
        }

        public static HashSet<T> Union<T>(this HashSet<T> first, HashSet<T> second)
        {
            var result = new HashSet<T>(first);
            result.UnionWith(second);
            return result;
        }
    }
}