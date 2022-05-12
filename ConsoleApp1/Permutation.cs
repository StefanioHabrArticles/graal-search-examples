using System;
using System.Linq;
using System.Text;

namespace ConsoleApp1
{
    public class Permutation : IEquatable<Permutation>
    {
        private readonly int[] _map;

        public Permutation(params int[] map)
        {
            _map = new int[map.Length];
            for (var i = 0; i < map.Length; i++)
            {
                _map[i] = map[i];
            }
        }

        public int this[int index] => _map[index];

        public bool Equals(Permutation other)
        {
            if (other != null && _map.Length == other._map.Length)
            {
                return _map.Zip(other._map)
                    .All(pair => pair.First == pair.Second);
            }

            return false;
        }

        public override string ToString() =>
            new StringBuilder()
                .AppendJoin(' ', Enumerable.Range(1, _map.Length))
                .Append('\n')
                .AppendJoin(' ', _map.Select(x => x + 1))
                .ToString();
    }
}