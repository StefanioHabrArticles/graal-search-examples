
namespace ConsoleApp2
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
            var richest = people.Sum();
            Console.WriteLine(richest);

            var predicates = new List<Predicate<char>>
            {
                x => x is >= '0' and <= '9',
                x => x is >= 'A' and <= 'Z',
                x => x is >= 'a' and <= 'z'
            }.Select<Predicate<char>, Any<char>>(x => x).ToList();
            var digitOrLetter = predicates.Sum();
            Console.WriteLine($"c {digitOrLetter.Test('a')} C {digitOrLetter.Test('C')} 5 {digitOrLetter.Test('5')}");
            
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

            var someDtoCatalogue = new Catalogue<SomeDto>()
                .Add(new SomeDto(1, false, "asa"))
                .Add(new SomeDto(2, true, "asa"))
                .Remove(new SomeDto(1, false, "asa"));
            Console.WriteLine(someDtoCatalogue.Reduce(_ => 1));
            var logs = someDtoCatalogue.ToList();
            logs.ForEach(Console.WriteLine);
            var someDtoObjects = someDtoCatalogue.Collect();
            someDtoObjects.ForEach(Console.WriteLine);
        }
    }
}