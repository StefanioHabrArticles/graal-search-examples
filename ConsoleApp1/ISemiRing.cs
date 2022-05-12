namespace ConsoleApp1
{
    public interface ISemiRing<T> : IMonoid<T>
    {
        T One { get; }

        T Times(T left, T right);
    }
}