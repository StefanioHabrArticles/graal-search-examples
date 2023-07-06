namespace ConsoleApp2;

public class All<T> : IGroup<All<T>>
{
    private readonly Predicate<T> _predicate;

    private All(Predicate<T> predicate) =>
        _predicate = predicate;

    public bool Test(T obj) =>
        _predicate(obj);

    public static All<T> operator +(All<T> left, All<T> right) =>
        new(x => left._predicate(x) && right._predicate(x));

    public static All<T> AdditiveIdentity =>
        new(_ => true);

    public static All<T> operator -(All<T> value) =>
        new(x => !value._predicate(x));
    
    public static explicit operator All<T>(Predicate<T> pred) =>
        new(pred);
}