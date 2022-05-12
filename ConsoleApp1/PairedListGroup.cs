using System.Diagnostics.CodeAnalysis;

namespace ConsoleApp1
{
    public struct PairedListGroup<T> : IGroup<PairedList<T>>
    {
        public PairedList<T> Plus(PairedList<T> left, PairedList<T> right)
        {
            var (left1, left2) = left;
            var (right1, right2) = right;
            var newLeft = left1.Except(right2).Union(right1.Except(left2));
            var newRight = left2.Except(right1).Union(right2.Except(left1));
            return new PairedList<T>(newLeft, newRight);
        }

        public PairedList<T> Zero => new ();
        
        public PairedList<T> Inverse(PairedList<T> item)
        {
            var (first, second) = item;
            return new PairedList<T>(second, first);
        }
    }
}