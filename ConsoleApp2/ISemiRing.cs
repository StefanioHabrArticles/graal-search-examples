using System.Numerics;

namespace ConsoleApp2;

public interface ISemiRing<T> :
    IAdditionOperators<T, T, T>,
    IAdditiveIdentity<T, T>,
    IMultiplyOperators<T, T, T>,
    IMultiplicativeIdentity<T, T>
    where T :
    IAdditionOperators<T, T, T>,
    IAdditiveIdentity<T, T>,
    IMultiplyOperators<T, T, T>,
    IMultiplicativeIdentity<T, T>
{
}