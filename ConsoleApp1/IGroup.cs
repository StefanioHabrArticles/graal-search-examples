namespace ConsoleApp1
{
    public interface IGroup<T> : IMonoid<T>
    {
        T Inverse(T item);
    }
}