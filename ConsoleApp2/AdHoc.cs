namespace ConsoleApp2;

class Appender
{
    public T AppendItems<TAppendable, T>(T a, T b)
        where TAppendable : struct, IAppendable<T> =>
        default(TAppendable).Append(a, b);

    public T AppendItems<T>(AppendableValue<T> a, AppendableValue<T> b) =>
        a.Append(b);
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

abstract class AppendableValue<T>
{
    public T Value { get; }

    protected AppendableValue(T value) =>
        Value = value;

    public abstract T Append(AppendableValue<T> item);
}

class AppendableIntValue :
    AppendableValue<int>
{
    public AppendableIntValue(int value) :
        base(value) { }

    public override int Append(AppendableValue<int> item) =>
        Value + item.Value;
}

class AppendableStringValue :
    AppendableValue<string>
{
    public AppendableStringValue(string value) :
        base(value) { }

    public override string Append(AppendableValue<string> item) =>
        $"{Value}{item.Value}";
}

class Example
{
    public void Test()
    {
        Console.WriteLine(new Appender().AppendItems<AppendableInt, int>(1, 2));
        Console.WriteLine(new Appender().AppendItems<AppendableString, string>("1", "2"));

        Console.WriteLine(new Appender().AppendItems(new AppendableIntValue(1), new AppendableIntValue(2)));
    }
}