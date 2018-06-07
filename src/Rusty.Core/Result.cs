using System;

namespace Rusty.Core
{
    public abstract class Result<T, E>
    {
        /// <summary>
        /// Returns `true` if the result is `Ok`.
        /// </summary>
        public abstract bool IsOk();

        /// <summary>
        /// Returns `true` if the result is `Err`.
        /// </summary>
        public bool IsErr() => !IsOk();

        /// <summary>
        /// Converts from `Result<T, E>` to `Option<T>`.
        /// </summary>
        public abstract Option<T> Some();

        /// <summary>
        /// Converts from `Result<T, E>` to `Option<T>`.
        /// </summary>
        public abstract Option<E> None();

        /// <summary>
        /// Maps a `Result<T, E>` to `Result<U, E>` by applying a function to a contained `Ok` value, leaving an `Err` value untouched.
        /// </summary>
        public abstract Result<U, E> Map<U>(in Func<T, U> f);

        /// <summary>
        /// Maps a `Result<T, E>` to `Result<T, F>` by applying a function to a contained `Err` value, leaving an `Ok` value untouched.
        /// </summary>
        public abstract Result<T, U> MapErr<U>(in Func<E, U> f);

        /// <summary>
        /// Returns `res` if the result is `Ok`, otherwise returns the `Err` value of self.
        /// </summary>
        public abstract Result<U, E> And<U>(in Result<U, E> res);

        /// <summary>
        /// Calls `op` if the result is `Ok`, otherwise returns the `Err` value of self.
        /// </summary>
        public abstract Result<U, E> AndThen<U>(in Func<T, Result<U, E>> op);

        /// <summary>
        /// Returns `res` if the result is `Err`, otherwise returns the `Ok` value of self.
        /// </summary>
        public abstract Result<T, E> Or(in Result<T, E> res);

        /// <summary>
        /// Calls `op` if the result is `Err`, otherwise returns the `Ok` value of self.
        /// </summary>
        public abstract Result<T, U> OrElse<U>(in Func<E, Result<T, U>> op);

        /// <summary>
        /// Unwraps a result, yielding the content of an `Ok`.
        /// </summary>
        /// <exception cref="Exception">Throw if the self value is an `Err`</exception>
        /// <exception cref="InvalidOperationException">Throw if the self value is an `Err`</exception>
        public abstract T Unwrap();

        /// <summary>
        /// Unwraps a result, yielding the content of an `Ok`. Else, it returns `optb`.
        /// </summary>
        public abstract T UnwrapOr(in T optb);

        /// <summary>
        /// Unwraps a result, yielding the content of an `Ok`. If the value is an `Err` then it calls `op` with its value.
        /// </summary>
        public abstract T UnwrapOrElse(in Func<E, T> op);

        public override string ToString() => $"Rusty.Core.Result({nameof(T)}, {nameof(E)})";
    }

    public sealed class Ok<T, E> : Result<T, E>
    {
        private readonly T _value;

        public Ok(in T v) => _value = v;

        public override bool IsOk() => true;

        public override Option<T> Some() => new Some<T>(_value);

        public override Option<E> None() => None<E>.Instance;

        public override Result<U, E> Map<U>(in Func<T, U> f) => new Ok<U, E>(f(_value));

        public override Result<T, U> MapErr<U>(in Func<E, U> f) => new Ok<T, U>(_value);

        public override Result<U, E> And<U>(in Result<U, E> res) => res;

        public override Result<U, E> AndThen<U>(in Func<T, Result<U, E>> op) => op(_value);

        public override Result<T, E> Or(in Result<T, E> res) => this;

        public override Result<T, U> OrElse<U>(in Func<E, Result<T, U>> op) => new Ok<T, U>(_value);

        public override T Unwrap() => _value;

        public override T UnwrapOr(in T optb) => _value;

        public override T UnwrapOrElse(in Func<E, T> op) => _value;

    }

    public sealed class Err<T, E> : Result<T, E>
    {
        private readonly E _error;

        public Err(in E e) => _error = e;

        public override bool IsOk() => false;

        public override Option<T> Some() => None<T>.Instance;

        public override Option<E> None() => new Some<E>(_error);

        public override Result<U, E> Map<U>(in Func<T, U> f) => new Err<U, E>(_error);

        public override Result<T, U> MapErr<U>(in Func<E, U> f) => new Err<T, U>(f(_error));

        public override Result<U, E> And<U>(in Result<U, E> res) => new Err<U, E>(_error);

        public override Result<U, E> AndThen<U>(in Func<T, Result<U, E>> op) => new Err<U, E>(_error);

        public override Result<T, E> Or(in Result<T, E> res) => res;

        public override Result<T, U> OrElse<U>(in Func<E, Result<T, U>> op) => op(_error);

        public override T Unwrap()
        {
            if (_error is Exception ex)
                throw ex;
            throw new InvalidOperationException($"{nameof(Err<T, E>)}: {_error}");
        }

        public override T UnwrapOr(in T optb) => optb;

        public override T UnwrapOrElse(in Func<E, T> op) => op(_error);
    }
}
