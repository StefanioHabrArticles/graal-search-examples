using System.Linq;

namespace ConsoleApp1
{
    public readonly struct SymmetricGroup : IGroup<Permutation>
    {
        private readonly int _length;

        public SymmetricGroup(int length)
        {
            _length = length;
        }

        public Permutation Plus(Permutation left, Permutation right)
        {
            var newMap = new int[_length];
            for (var i = 0; i < _length; i++)
            {
                newMap[i] = right[left[i]];
            }

            return new Permutation(newMap);
        }

        public Permutation Zero =>
            new(Enumerable.Range(0, _length).ToArray());

        public Permutation Inverse(Permutation item)
        {
            var newMap = new int[_length];
            for (var i = 0; i < _length; i++)
            {
                newMap[item[i]] = i;
            }

            return new Permutation(newMap);
        }
    }
}