namespace ConsoleApp2;

public interface IDoubleSemiRing<T> : ISemiRing<T>
    where T : IDoubleSemiRing<T>
{
    static abstract explicit operator double(T item);

    static abstract implicit operator T(double value);
}