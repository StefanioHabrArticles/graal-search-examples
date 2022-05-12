namespace ConsoleApp1
{
    public interface IRing<T> : IGroup<T>
    {
        T One { get; }
        
        T Times(T left, T right);
    }

    public static class RingExtensions
    {
        public static T Power<T>(this IRing<T> ring, T item, int n)
        {
            var result = item;
            for (var i = 1; i < n; i++)
            {
                result = ring.Times(result, item);
            }

            return result;
        }
    }
}