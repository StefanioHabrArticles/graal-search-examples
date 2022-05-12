using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    internal static class Program
    {
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            var people = new List<Person>
            {
                new(),
                new("Bob", 1000),
                new("Tim", 1239),
                new("Jeff", 2000000000)
            };
            var richest = people.Sum<Person, Max<Person>>();
            Console.WriteLine(richest);
            
            var predicates = new List<Predicate<char>>
            {
                x => x is >= '0' and <= '9',
                x => x is >= 'A' and <= 'Z',
                x => x is >= 'a' and <= 'z'
            };
            var digitOrLetter = predicates.Sum<Predicate<char>, Any<char>>();
            Console.WriteLine($"c {digitOrLetter('a')} C {digitOrLetter('C')} 5 {digitOrLetter('5')}");

            var strings = new List<string> {"foo", "foo", "foo", "bar", "bar", "baz", "pipi", "pupu"};
            var dicts = strings
                .Select(x => new Dictionary<string, int> {{x, 1}});
            var map = dicts
                .Sum<Dictionary<string, int>, MapMonoid<string, int, IntSemiGroup>>();
            Console.WriteLine(string.Join(" ", map));

            var anotherDicts = strings
                .Select(x => new Dictionary<int, List<string>>
                {
                    {x.Length, new List<string> {x}}
                });
            var anotherMap = anotherDicts
                .Sum<Dictionary<int, List<string>>, MapMonoid<int, List<string>, ListSemiGroup<string>>>();
            Console.WriteLine(string.Join(" ", anotherMap.Select(x => $"[{x.Key}, [{string.Join(" ", x.Value)}]]")));

            var stringsCatalogue = new Catalogue<string>()
                .Add("cabcc")
                .Pass()
                .Remove("cc")
                .Remove("a")
                .Add("ed");
            Console.WriteLine(stringsCatalogue.Reduce<int, IntAddGroup>(s => s.Length));
            var chars = stringsCatalogue.Reduce<PairedList<char>, PairedListGroup<char>>(
                s => new PairedList<char>(s)
            ).ToList();
            Console.WriteLine(string.Join("", chars));

            var someDtosCatalogue = new Catalogue<SomeDto>()
                .Add(new SomeDto(1, false, "asa"))
                .Add(new SomeDto(2, true, "asa"))
                .Remove(new SomeDto(1, false, "asa"));
            Console.WriteLine(someDtosCatalogue.Reduce<int, IntAddGroup>(_ => 1));
            var logs = someDtosCatalogue.ToList();
            logs.ForEach(Console.WriteLine);
            var someDtosObjects = someDtosCatalogue.Collect();
            someDtosObjects.ForEach(Console.WriteLine);

            var end = default(End<PairedHashSet<int>, PairedHashSetGroup<int>>);
            var id = end.One;
            PairedHashSet<int> X2(PairedHashSet<int> s) => new(s.ToHashSet().Select(i => i * 2));
            var idPlusX2 = end.Plus(id, X2);
            var pows = end.Power(idPlusX2, 6)(new(1));
            foreach (var pow in pows.ToHashSet())
            {
                Console.WriteLine(pow);
            }

            PairedHashSet<int> Inc(PairedHashSet<int> s) => new(s.ToHashSet().Select(x => x + 1));
            var range = end.Power(end.Plus(end.One, Inc), 10);
            var items = range(new (1));
            foreach (var i in items.ToHashSet())
            {
                Console.WriteLine(i);
            }
            
            var s3 = new SymmetricGroup(3);
            var g = new Permutation(1, 2, 0);
            Console.WriteLine(g);
            Console.WriteLine(s3.Plus(g, s3.Inverse(g)).Equals(s3.Zero));
            Console.WriteLine(new SymmetricGroup(5).Inverse(new Permutation(1, 4, 3, 2, 0)));

            var afMonoid = default(AffineFunctionMonoid<int, IntRing>);
            var af = afMonoid.Plus(new(1, 5), new(2, -1));
            Console.WriteLine(af);
            Console.WriteLine(af.Apply(7));

            SquareMatrix<int, IntRing> testMatrix = new(3,
                new[] {1, 0, 3},
                new[] {0, 5, 0},
                new[] {2, 0, 6}
            );
            Console.WriteLine(testMatrix.Power(4));

            var weightedGraph = new WeightedGraph(4,
                (0, new() {(1, 3), (2, 8), (3, 12)}),
                (1, new() {(2, 2), (3, 10)}),
                (2, new() {(3, 3)})
            );

            Console.WriteLine(weightedGraph.GetShortestPath(0, 3, 3));
            Console.WriteLine(weightedGraph.GetLongestPath(0, 3, 2));
        }

        private struct IntAddGroup : IGroup<int>
        {
            public int Plus(int left, int right) => left + right;

            public int Zero => 0;

            public int Inverse(int item) => -item;
        }

        private struct IntSemiGroup : ISemiGroup<int>
        {
            public int Plus(int left, int right) => left + right;
        }

        private struct ListSemiGroup<T> : ISemiGroup<List<T>>
        {
            public List<T> Plus(List<T> left, List<T> right)
            {
                var result = new List<T>(left);
                result.AddRange(right);
                return result;
            }
        }
        
        private struct IntRing : IRing<int>, ISemiRing<int>
        {
            public int Plus(int left, int right) => left + right;

            public int Zero => 0;
            
            public int Inverse(int item) => -item;

            public int One => 1;

            public int Times(int left, int right) => left * right;
        }
    }
}