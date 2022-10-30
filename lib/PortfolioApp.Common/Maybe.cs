using System;
using System.Threading.Tasks;

namespace PortfolioApp.Common
{
    public class Maybe
    {
        protected Maybe() { }
        protected Maybe(Error error)
        {
            Error = error;
        }

        public Error? Error { get; }
        
        public static Maybe None { get; } = new();

        public virtual bool IsNone => Error is null;
        public bool HasError => Error is not null;
    }

    public sealed class Maybe<TValue> : Maybe
    {
        private Maybe(Error error): base(error) { }
        private Maybe(TValue value) 
        {
            Value = value;
        }

        public TValue? Value { get; }

        public override bool IsNone => Value is null;
        public bool HasValue => Value is not null;

        public T Match<T>(Func<TValue, T> onSome, Func<Error, T> onError) => HasValue ? onSome(Value!) : onError(Error!);
        public T Match<T>(Func<TValue, T> onSome, Func<T> onNone) => HasValue ? onSome(Value!) : onNone();

        public Task<T> Match<T>(Func<TValue, Task<T>> onSome, Func<Error, Task<T>> onError) => HasValue ? onSome(Value!) : onError(Error!);
        public Task<T> Match<T>(Func<TValue, Task<T>> onSome, Func<Task<T>> onNone) => HasValue ? onSome(Value!) : onNone();

        public static implicit operator Maybe<TValue>(TValue value) => new(value);
        public static implicit operator Maybe<TValue>(Error error) => new(error);
    }
 }
