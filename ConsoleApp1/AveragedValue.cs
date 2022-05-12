namespace ConsoleApp1
{
    public class AveragedValue
    {
        private double _sum;
        private int _count;

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

        public static AveragedValue operator -(AveragedValue av) => new (-av._sum, -av._count);
    }
    
    public struct Avg : IGroup<AveragedValue>
    {
        public AveragedValue Plus(AveragedValue left, AveragedValue right) => left + right;

        public AveragedValue Zero => new ();
        
        public AveragedValue Inverse(AveragedValue item) => -item;
    }
}