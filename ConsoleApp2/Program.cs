
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

            var someDtosCatalogue = new Catalogue<SomeDto>()
                .Add(new SomeDto(1, false, "asa"))
                .Add(new SomeDto(2, true, "asa"))
                .Remove(new SomeDto(1, false, "asa"));
            Console.WriteLine(someDtosCatalogue.Reduce(_ => 1));
            var logs = someDtosCatalogue.ToList();
            logs.ForEach(Console.WriteLine);
            var someDtosObjects = someDtosCatalogue.Collect();
            someDtosObjects.ForEach(Console.WriteLine);
        }
    }
}