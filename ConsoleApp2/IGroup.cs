using System.Numerics;

namespace ConsoleApp2
{
    public interface IGroup<T> :
        IAdditionOperators<T, T, T>,
        IAdditiveIdentity<T, T>,
        IUnaryNegationOperators<T, T>
        where T :
        IAdditionOperators<T, T, T>,
        IAdditiveIdentity<T, T>,
        IUnaryNegationOperators<T, T>
    {
    }
}