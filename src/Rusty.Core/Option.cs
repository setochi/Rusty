using System;

namespace Rusty.Core
{
    public abstract class Option<T>
    {
        /// <summary>
        /// Returns `true` if the option is a `Some` value.
        /// </summary>
        public abstract bool IsSome();

        /// <summary>
        /// Return `true` if the option is a `None` value.
        /// </summary>
        public bool IsNone() => !IsSome();

        /// <summary>
        /// Unwraps an option, yielding the content of a `Some`.
        /// </summary>
        /// <exception cref="InvalidOperationException">Throw exception if the value is a `None` with a custom throw message provided by `msg`.</exception>
        public abstract T Expect(in string msg);

        /// <summary>
        /// Moves the value `v` out of the `Option<T>` if it is `Some(v)`.
        /// </summary>
        /// <exception cref="InvalidOperationException">Throw if the self value equals `None`</exception>
        public abstract T Unwrap();

        /// <summary>
        /// Returns the contained value or a default.
        /// </summary>
        public abstract T UnwrapOr(in T def);

        /// <summary>
        /// Returns the contained value or computes it form a closure.
        /// </summary>
        public abstract T UnwrapOrElse(in Func<T> f);

        /// <summary>
        /// Maps as `Option<T>` to `Option<U>` by applying a function to a contained value.
        /// </summary>
        public abstract Option<U> Map<U>(in Func<T, U> f);

        /// <summary>
        /// Applies a function to the contained value (if any), or returns a `default`(if not).
        /// </summary>
        public abstract U MapOr<U>(in U def, in Func<T, U> f);


        /// <summary>
        /// Applies a function to the contained value (if any).
        /// </summary>
        public abstract U MapOrElse<U>(in Func<U> def, in Func<T, U> f);

        /// <summary>
        /// Transforms the `Option<T>` into a `Result<T, E>`, mapping `Some(v)` to `Ok(v)` and `None` to `Err(err)`.
        /// </summary>
        public abstract Result<T, E> OkOr<E>(in E err);

        /// <summary>
        /// Transforms the `Option<T>` into a `Result<T, E>`, mapping `Some(v)` to `Ok(v)` and `None` to `Err(err)`.
        /// </summary>
        public abstract Result<T, E> OkOrElse<E>(in Func<E> err);

        /// <summary>
        /// Returns `None` if the option is `None`, otherwise returns `optb`.
        /// </summary>
        public abstract Option<U> And<U>(in Option<U> optb);

        /// <summary>
        /// Returns `None` if the option is `None`, otherwise calls `f`with the wrapped value and return the result.
        /// </summary>
        public abstract Option<U> AndThen<U>(in Func<T, Option<U>> f);

        /// <summary>
        /// Returns `None` if the option is `None`, otherwise calls `predicate` with the wrapped value and returns:
        /// - `Some(v)` if `predicate` returns `true` (where `t` is the wrapped value), and
        /// - `None` if `predicate` returns `false`.
        /// </summary>
        public abstract Option<T> Filter(in Func<T, bool> predicate);

        /// <summary>
        /// Returns the option if it contains a value, otherwise returns `optb`.
        /// </summary>
        public abstract Option<T> Or(in Option<T> optb);

        /// <summary>
        /// Returns the option if it contains a value, otherwise calls `f` and returns the result.
        /// </summary>
        public abstract Option<T> OrElse(in Func<Option<T>> f);

        public override string ToString() => $"Rusty.Core.Option({nameof(T)})";
    }

    public sealed class Some<T> : Option<T>
    {
        private readonly T _value;

        public Some(in T v) => _value = v;

        public override bool IsSome() => true;

        public override T Expect(in string msg) => _value;

        public override T Unwrap() => _value;

        public override T UnwrapOr(in T def) => _value;

        public override T UnwrapOrElse(in Func<T> f) => _value;

        public override Option<U> Map<U>(in Func<T, U> f) => new Some<U>(f(_value));

        public override U MapOr<U>(in U def, in Func<T, U> f) => f(_value);

        public override U MapOrElse<U>(in Func<U> def, in Func<T, U> f) => f(_value);

        public override Result<T, E> OkOr<E>(in E err) => new Ok<T, E>(_value);

        public override Result<T, E> OkOrElse<E>(in Func<E> err) => new Ok<T, E>(_value);

        public override Option<U> And<U>(in Option<U> optb) => optb;

        public override Option<U> AndThen<U>(in Func<T, Option<U>> f) => f(_value);

        public override Option<T> Filter(in Func<T, bool> predicate)
        {
            if (predicate(_value))
                return this;
            return None<T>.Instance;
        }

        public override Option<T> Or(in Option<T> optb) => this;

        public override Option<T> OrElse(in Func<Option<T>> f) => this;
    }

    public sealed class None<T> : Option<T>
    {
        public static None<T> Instance { get; } = new None<T>();

        private None() { }

        public override bool IsSome() => false;

        public override T Expect(in string msg) => throw new InvalidOperationException(msg);

        public override T Unwrap() => throw new InvalidOperationException(nameof(None<T>));

        public override T UnwrapOr(in T def) => def;

        public override T UnwrapOrElse(in Func<T> f) => f();

        public override Option<U> Map<U>(in Func<T, U> f) => None<U>.Instance;

        public override U MapOr<U>(in U def, in Func<T, U> f) => def;

        public override U MapOrElse<U>(in Func<U> def, in Func<T, U> f) => def();

        public override Result<T, E> OkOr<E>(in E err) => new Err<T, E>(err);

        public override Result<T, E> OkOrElse<E>(in Func<E> err) => new Err<T, E>(err());

        public override Option<U> And<U>(in Option<U> optb) => None<U>.Instance;

        public override Option<U> AndThen<U>(in Func<T, Option<U>> f) => None<U>.Instance;

        public override Option<T> Filter(in Func<T, bool> predicate) => this;

        public override Option<T> Or(in Option<T> optb) => optb;

        public override Option<T> OrElse(in Func<Option<T>> f) => f();
    }
}
