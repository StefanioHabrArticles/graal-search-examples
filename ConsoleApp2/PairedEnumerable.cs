using System.Numerics;

namespace ConsoleApp2
{
    public class PairedEnumerable<E, W, T> :
        IAdditionOperators<PairedEnumerable<E, W, T>, PairedEnumerable<E, W, T>, PairedEnumerable<E, W, T>>,
        IAdditiveIdentity<PairedEnumerable<E, W, T>, PairedEnumerable<E, W, T>>,
        IUnaryNegationOperators<PairedEnumerable<E, W, T>, PairedEnumerable<E, W, T>>
        where W : Wrapper<E, T>, new()
        where E : class, IEnumerable<T>, new()
    {
        public W First { get; }

        public W Second { get; }

        private PairedEnumerable(E first, E second)
        {
            First = new W { Enumerable = first };
            Second = new W { Enumerable = second };
        }

        private PairedEnumerable() :
            this(new E(), new E())
        {
        }

        private PairedEnumerable(E enumerable) :
            this(enumerable, new E())
        {
        }

        public void Deconstruct(out W first, out W second)
        {
            first = First;
            second = Second;
        }

        public E Merge() =>
            First.Except(Second).Enumerable;

        public override string ToString()
            => string.Join("\n", Merge());

        public static implicit operator PairedEnumerable<E, W, T>(E enumerable) =>
            new(enumerable);

        public static PairedEnumerable<E, W, T> operator +(
            PairedEnumerable<E, W, T> left,
            PairedEnumerable<E, W, T> right)
        {
            var (left1, left2) = left;
            var (right1, right2) = right;
            var newLeft = left1.Except(right2).Union(right1.Except(left2));
            var newRight = left2.Except(right1).Union(right2.Except(left1));
            return new PairedEnumerable<E, W, T>(newLeft.Enumerable, newRight.Enumerable);
        }

        public static PairedEnumerable<E, W, T> AdditiveIdentity => new();

        public static PairedEnumerable<E, W, T> operator -(PairedEnumerable<E, W, T> value)
        {
            var (first, second) = value;
            return new PairedEnumerable<E, W, T>(second.Enumerable, first.Enumerable);
        }
    }

    public abstract class Wrapper<E, T>
        where E : IEnumerable<T>, new()
    {
        public E Enumerable { get; init; }

        protected Wrapper(E enumerable)
        {
            Enumerable = enumerable;
        }

        public abstract Wrapper<E, T> Union(Wrapper<E, T> that);

        public abstract Wrapper<E, T> Except(Wrapper<E, T> that);
    }

    public class ListWrapper<T> : Wrapper<List<T>, T>
    {
        public ListWrapper() : base(new List<T>())
        {
        }

        private ListWrapper(List<T> list) : base(list)
        {
        }

        public override Wrapper<List<T>, T> Union(Wrapper<List<T>, T> that)
        {
            var list = new List<T>(Enumerable);
            list.AddRange(that.Enumerable);
            return new ListWrapper<T>(list);
        }

        public override Wrapper<List<T>, T> Except(Wrapper<List<T>, T> that)
        {
            /*var list = new List<T>(Enumerable);
            that.Enumerable.ForEach(item => list.Remove(item));
            return new ListWrapper<T>(list);*/
            var result = new List<T>(Enumerable);
            that.Enumerable.ForEach(item =>
            {
                var li = result.LastIndexOf(item);
                if (li > -1)
                {
                    result.RemoveAt(li);
                }
            });
            return new ListWrapper<T>(result);
        }
    }

    public class HashSetWrapper<T> : Wrapper<HashSet<T>, T>
    {
        public HashSetWrapper() : this(new HashSet<T>())
        {
        }

        private HashSetWrapper(HashSet<T> hashSet) : base(hashSet)
        {
        }

        public override Wrapper<HashSet<T>, T> Union(Wrapper<HashSet<T>, T> that)
        {
            var hashSet = new HashSet<T>(Enumerable);
            hashSet.UnionWith(that.Enumerable);
            return new HashSetWrapper<T>(hashSet);
        }

        public override Wrapper<HashSet<T>, T> Except(Wrapper<HashSet<T>, T> that)
        {
            var hashSet = new HashSet<T>(Enumerable);
            hashSet.ExceptWith(that.Enumerable);
            return new HashSetWrapper<T>(hashSet);
        }
    }
}