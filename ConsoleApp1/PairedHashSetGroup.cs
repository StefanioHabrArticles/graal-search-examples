namespace ConsoleApp1
{
    public struct PairedHashSetGroup<T> : IGroup<PairedHashSet<T>>
    {
        public PairedHashSet<T> Plus(PairedHashSet<T> left, PairedHashSet<T> right)
        {
            var (left1, left2) = left;
            var (right1, right2) = right;
            var newLeft = left1.Except(right2).Union(right1.Except(left2));
            var newRight = left2.Except(right1).Union(right2.Except(left1));
            return new PairedHashSet<T>(newLeft, newRight);
        }

        public PairedHashSet<T> Zero => new ();
        
        public PairedHashSet<T> Inverse(PairedHashSet<T> item)
        {
            var (first, second) = item;
            return new PairedHashSet<T>(second, first);
        }
    }
}