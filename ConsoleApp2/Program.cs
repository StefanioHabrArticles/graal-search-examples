namespace ConsoleApp2
{
    internal static class Program
    {
        // ReSharper disable once UnusedParameter.Local
        private static void Main(string[] args)
        {
            var people = new List<Person>
            {
                Person.AdditiveIdentity,
                new("Bob", 1000),
                new("Tim", 1239),
                new("Jeff", 2000000000)
            };
            var richest = people.Sum();
            Console.WriteLine(richest.Name);

            var digitOrLetter = new List<Predicate<char>>
                {
                    x => x is >= '0' and <= '9',
                    x => x is >= 'A' and <= 'Z',
                    x => x is >= 'a' and <= 'z'
                }
                .Select(x => (Any<char>)x)
                .ToList().Sum();
            Console.WriteLine($"$ - {digitOrLetter.Test('$')}; Z - {digitOrLetter.Test('Z')}; 5 - {digitOrLetter.Test('5')}");

            var stringsCatalogue = new Catalogue<string>()
                .Add("cabcc")
                .Pass()
                .Remove("cc")
                .Remove("a")
                .Add("ed");
            Console.WriteLine(stringsCatalogue.Reduce(s => s.Length));
            var chars = stringsCatalogue.Reduce<PairedEnumerable<List<char>, ListWrapper<char>, char>>(
                s => s.ToList()
            ).Merge();
            Console.WriteLine(string.Join("", chars));

            var someDtoCatalogue = new Catalogue<SomeDto>
            {
                new SomeDto(1, false, "asa"),
                new SomeDto(2, true, "asa")
            }.Remove(new SomeDto(1, false, "asa"));
            Console.WriteLine(someDtoCatalogue.Reduce(_ => 1));
            var logs = someDtoCatalogue.ToList();
            logs.ForEach(Console.WriteLine);
            var someDtoObjects = someDtoCatalogue.Collect();
            someDtoObjects.ForEach(Console.WriteLine);

            var filter = new Catalogue<Predicate<int>>()
                .Add(x => x > 2)
                .Remove(x => x > 5)
                .Reduce(x => (All<int>)x);
            Console.WriteLine($"4 - {filter.Test(4)}; 6 - {filter.Test(6)}");

            SquareMatrix<int> testMatrix = new(3,
                new[] { 1, 0, 3 },
                new[] { 0, 5, 0 },
                new[] { 2, 0, 6 }
            );
            Console.WriteLine(testMatrix ^ 4);

            var weightedGraph = new WeightedGraph(4,
                (0, new() { (1, 3), (2, 8), (3, 12) }),
                (1, new() { (2, 2), (3, 10) }),
                (2, new() { (3, 3) })
            );

            Console.WriteLine(weightedGraph);
            Console.WriteLine(weightedGraph.GetShortestPath(0, 3, 3));
            Console.WriteLine(weightedGraph.GetLongestPath(0, 3, 2));
        }
    }
}