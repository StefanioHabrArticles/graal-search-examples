namespace ConsoleApp2;

class Appender
{
    public T AppendItems<TAppendable, T>(T a, T b)
        where TAppendable : struct, IAppendable<T> =>
        default(TAppendable).Append(a, b);
}

interface IAppendable<T>
{
    T Append(T a, T b);
}

struct AppendableInt : IAppendable<int>
{
    public int Append(int a, int b) =>
        a + b;
}

struct AppendableString : IAppendable<string>
{
    public string Append(string a, string b) =>
        $"{a}{b}";
}

class Example
{
    public void Test()
    {
        Console.WriteLine(new Appender().AppendItems<AppendableInt, int>(1, 2));
        Console.WriteLine(new Appender().AppendItems<AppendableString, string>("1", "2"));
    }
}