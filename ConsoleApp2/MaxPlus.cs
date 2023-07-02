using static System.Math;

namespace ConsoleApp2;

public class MaxPlus : IDoubleSemiRing<MaxPlus>
{
    private readonly double _value;

    private MaxPlus(double value) =>
        _value = value;

    public static MaxPlus operator +(MaxPlus left, MaxPlus right) =>
        Max(left._value, right._value);

    public static MaxPlus AdditiveIdentity =>
        double.NegativeInfinity;

    public static MaxPlus operator *(MaxPlus left, MaxPlus right) =>
        left._value + right._value;

    public static MaxPlus MultiplicativeIdentity => 0d;

    public static explicit operator double(MaxPlus item) =>
        item._value;

    public static implicit operator MaxPlus(double value) =>
        new(value);
}