using static System.Math;

namespace ConsoleApp1
{
    public struct MinPlus : ISemiRing<double>
    {
        public double Plus(double left, double right) => Min(left, right);

        public double Zero => double.PositiveInfinity;
        
        public double One => 0;

        public double Times(double left, double right) => left + right;
    }
}