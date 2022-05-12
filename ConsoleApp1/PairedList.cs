using System.Collections.Generic;

namespace ConsoleApp1
{
    public record PairedList<T>(List<T> First, List<T> Second)
    {
        public PairedList() : this(new(), new())
        {
        }

        public PairedList(IEnumerable<T> single) : this(new(single), new())
        {
        }

        public PairedList(T item) : this(new List<T> {item})
        {
        }
    }

    public static class PairedListExtensions
    {
        public static List<T> ToList<T>(this PairedList<T> pairedList)
        {
            var (first, second) = pairedList;
            return first.Except(second);
        }
    }

    public static class ListExtensions
    {
        public static List<T> Union<T>(this List<T> list, List<T> items)
        {
            var result = new List<T>(list);
            result.AddRange(items);
            return result;
        }
        
        public static List<T> Except<T>(this List<T> first, List<T> second)
        {
            var result = new List<T>(first);
            second.ForEach(item =>
            {
                var li = result.LastIndexOf(item);
                if (li > -1)
                {
                    result.RemoveAt(li);
                }
            });
            return result;
        }
    }
}