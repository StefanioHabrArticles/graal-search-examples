using System;

namespace ConsoleApp1
{
    public class FunctionStructure<A, B>
    {
        private readonly Func<A, B> _function;

        public static FunctionStructure<T, T> Identity<T>() => new (x => x);

        public FunctionStructure(Func<A, B> function)
        {
            _function = function;
        }

        public FunctionStructure<A, C> Compose<C>(FunctionStructure<B, C> other)
        {
            return new (x => other._function(_function(x)));
        }

        public B Invoke(A a) => _function(a);
    }
}