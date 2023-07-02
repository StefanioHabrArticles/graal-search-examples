using System.Numerics;

namespace ConsoleApp2
{
    public class AveragedValue :
        IAdditionOperators<AveragedValue, AveragedValue, AveragedValue>,
        IAdditiveIdentity<AveragedValue, AveragedValue>,
        IUnaryNegationOperators<AveragedValue, AveragedValue>
    {
        private readonly double _sum;
        private readonly int _count;

        public AveragedValue() : this(0, 0)
        {
        }

        public AveragedValue(double sum, int count = 1)
        {
            _sum = sum;
            _count = count;
        }

        public double Get() => _sum / _count;

        public static AveragedValue operator +(AveragedValue av1, AveragedValue av2)
        {
            var newCount = av1._count + av2._count;
            var newSum = av1._sum + av2._sum;

            return new AveragedValue(newSum, newCount);
        }

        public static AveragedValue AdditiveIdentity => new();

        public static AveragedValue operator -(AveragedValue av) =>
            new(-av._sum, -av._count);
    }
}