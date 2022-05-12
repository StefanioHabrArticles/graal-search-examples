namespace ConsoleApp1
{
    public interface IModule<T, S, R> : IGroup<T>
        where R : struct, IRing<S>
    {
        R Scalar => default;

        T ScalarTimes(S scalar, T item);
    }
}