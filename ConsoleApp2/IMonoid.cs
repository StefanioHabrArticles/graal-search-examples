using System.Numerics;

namespace ConsoleApp2
{
    public interface IMonoid<T> :
        IAdditionOperators<T, T, T>,
        IAdditiveIdentity<T, T>
        where T :
        IAdditionOperators<T, T, T>,
        IAdditiveIdentity<T, T>
    {
    }
}