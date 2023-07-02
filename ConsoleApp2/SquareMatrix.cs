using System.Collections;
using System.Numerics;
using System.Text;

namespace ConsoleApp2
{
    public class SquareMatrix<TSemiRing> : IEnumerable<SquareMatrix<TSemiRing>.Vector>
        where TSemiRing :
        IAdditionOperators<TSemiRing, TSemiRing, TSemiRing>,
        IAdditiveIdentity<TSemiRing, TSemiRing>,
        IMultiplyOperators<TSemiRing, TSemiRing, TSemiRing>,
        IMultiplicativeIdentity<TSemiRing, TSemiRing>
    {
        private readonly int _size;
        private readonly List<Vector> _rows = new();

        public SquareMatrix(int size)
        {
            _size = size;
            for (var i = 0; i < _size; i++)
            {
                _rows.Add(new Vector(
                    Enumerable.Repeat(TSemiRing.AdditiveIdentity, _size)
                ));
            }
        }

        public SquareMatrix(int size, params IEnumerable<TSemiRing>[] rows)
        {
            _size = size;
            _rows.AddRange(rows.Select(x => new Vector(x)));
        }

        public TSemiRing this[int i, int j]
        {
            get => _rows[i][j];
            set => _rows[i][j] = value;
        }

        public IEnumerator<Vector> GetEnumerator() => _rows.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString() =>
            new StringBuilder()
                .AppendJoin('\n', _rows.Select(row =>
                    new StringBuilder()
                        .Append('|')
                        .AppendJoin(", ", row)
                        .Append('|')))
                .ToString();

        public static SquareMatrix<TSemiRing> operator ~(SquareMatrix<TSemiRing> matrix)
        {
            var columns = Enumerable
                .Range(0, matrix._size)
                .Select(i => matrix.Select(col => col[i]));
            return new(matrix._size, columns.ToArray());
        }

        public static SquareMatrix<TSemiRing> operator *(
            SquareMatrix<TSemiRing> left,
            SquareMatrix<TSemiRing> right)
        {
            if (left._size != right._size)
                throw new InvalidOperationException();
            var transposed = ~right;
            var rows = left
                .Select(x => transposed.Select(y => y * x));
            return new(left._size, rows.ToArray());
        }

        public static SquareMatrix<TSemiRing> operator ^(
            SquareMatrix<TSemiRing> matrix,
            int power) =>
            Enumerable.Range(1, power - 1)
                .Select(_ => matrix)
                .Aggregate(matrix, (x, y) => x * y);

        public class Vector : IEnumerable<TSemiRing>
        {
            private readonly List<TSemiRing> _items;

            public Vector(IEnumerable<TSemiRing> items)
            {
                _items = new List<TSemiRing>(items);
            }

            public TSemiRing this[int index]
            {
                get => _items[index];
                set => _items[index] = value;
            }

            public IEnumerator<TSemiRing> GetEnumerator() =>
                _items.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() =>
                GetEnumerator();

            public static TSemiRing operator *(Vector left, Vector right) =>
                left.Zip(right)
                    .Select(pair => pair.First * pair.Second)
                    .Sum();
        }
    }
}