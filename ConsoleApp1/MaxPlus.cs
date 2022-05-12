using static System.Math;

namespace ConsoleApp1
{
    public struct MaxPlus : ISemiRing<double>
    {
        public double Plus(double left, double right) => Max(left, right);

        public double Zero => double.NegativeInfinity;
        
        public double One => 0;

        public double Times(double left, double right) => left + right;
    }
}