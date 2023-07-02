using static System.Math;

namespace ConsoleApp2;

public class MinPlus : IDoubleSemiRing<MinPlus>
{
    private readonly double _value;

    private MinPlus(double value) =>
        _value = value;

    public static MinPlus operator +(MinPlus left, MinPlus right) =>
        Min(left._value, right._value);

    public static MinPlus AdditiveIdentity =>
        double.PositiveInfinity;

    public static MinPlus operator *(MinPlus left, MinPlus right) =>
        left._value + right._value;

    public static MinPlus MultiplicativeIdentity => 0d;

    public static explicit operator double(MinPlus item) =>
        item._value;

    public static implicit operator MinPlus(double value) =>
        new(value);
}