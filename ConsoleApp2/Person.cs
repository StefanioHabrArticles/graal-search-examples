namespace ConsoleApp2
{
    public record Person(string Name, int Money) :
        IComparable<Person>,
        IMonoid<Person>
    {
        public int CompareTo(Person? other) =>
            Money.CompareTo(other?.Money);

        public static Person operator +(Person left, Person right) =>
            left.CompareTo(right) > 0
                ? left
                : right;

        public static Person AdditiveIdentity =>
            new(string.Empty, default);
    }
}