using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class SquareMatrix<T, S> : IEnumerable<SquareMatrix<T, S>.Vector>
        where S : struct, ISemiRing<T>
    {
        private readonly int _size;
        private readonly List<Vector> _rows = new();

        public SquareMatrix(int size)
        {
            _size = size;
            for (var i = 0; i < _size; i++)
            {
                _rows.Add(new Vector(
                    Enumerable.Repeat(default(S).Zero, _size)
                ));
            }
        }

        public SquareMatrix(int size, params IEnumerable<T>[] rows)
        {
            _size = size;
            _rows.AddRange(rows.Select(x => new Vector(x)));
        }

        public T this[int i, int j]
        {
            get => _rows[i][j];
            set => _rows[i][j] = value;
        }

        public SquareMatrix<T, S> Transpose()
        {
            var columns = Enumerable
                .Range(0, _size)
                .Select(i => _rows.Select(col => col[i]));
            return new (_size, columns.ToArray());
        }

        public SquareMatrix<T, S> Product(SquareMatrix<T, S> that)
        {
            var transposed = that.Transpose();
            var rows = this
                .Select(x => transposed.Select(x.Dot));
            return new (_size, rows.ToArray());
        }

        public SquareMatrix<T, S> Power(int n)
        {
            var result = this;
            for (var i = 1; i < n; i++)
            {
                result = result.Product(this);
            }

            return result;
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

        public class Vector : IEnumerable<T>
        {
            private readonly List<T> _items;

            public Vector(IEnumerable<T> items)
            {
                _items = new List<T>(items);
            }

            public T this[int index]
            {
                get=> _items[index];
                set => _items[index] = value;
            }

            public T Dot(Vector that) => _items
                .Zip(that._items)
                .Select(pair => default(S).Times(pair.First, pair.Second))
                .Sum<T, S>();

            public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}