using System;

namespace ConsoleApp1
{
    public record Person(string Name, int Money) : IComparable<Person>
    {
        public Person() : this("", int.MinValue)
        {
        }

        public int CompareTo(Person other) => Money.CompareTo(other.Money);
    }
}