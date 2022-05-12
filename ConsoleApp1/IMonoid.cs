namespace ConsoleApp1
{
    public interface IMonoid<T> : ISemiGroup<T>
    {
        T Zero { get; }
    }
}