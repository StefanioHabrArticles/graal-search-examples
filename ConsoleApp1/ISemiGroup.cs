namespace ConsoleApp1
{
    public interface ISemiGroup<T>
    {
        T Plus(T left, T right);
    }
}