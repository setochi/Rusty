using System;

namespace Rusty.Core
{
    /// <summary>
    /// Result Operator
    /// </summary>
    public static class Result
    {
        public static Result<T, ArgumentNullException> Wrap<T>(in T? v) where T : struct
        {
            if (v.HasValue)
                return new Ok<T, ArgumentNullException>(v.Value);
            return new Err<T, ArgumentNullException>(new ArgumentNullException(nameof(v)));
        }

        public static Result<T, ArgumentNullException> Wrap<T>(in T v) where T : class
        {
            if (v != null)
                return new Ok<T, ArgumentNullException>(v);
            return new Err<T, ArgumentNullException>(new ArgumentNullException(nameof(v)));
        }

        public static Result<TResult, Exception> Wrap<TResult>(in Func<TResult> f)
        {
            try
            {
                var value = f();
                if (value != null)
                    return new Ok<TResult, Exception>(value);
                return new Err<TResult, Exception>(new ArgumentException($"Calling {nameof(f)} has succeeded. But returned value is a null."));
            }
            catch (Exception ex)
            {
                return new Err<TResult, Exception>(ex);
            }
        }

        public static Result<TResult, Exception> Wrap<T, TResult>(in Func<T, TResult> f, in T arg)
        {
            try
            {
                var value = f(arg);
                if (value != null)
                    return new Ok<TResult, Exception>(value);
                return new Err<TResult, Exception>(new ArgumentException($"Calling {nameof(f)} has succeeded. But returned value is a null. Call with arg: {arg}."));
            }
            catch (Exception ex)
            {
                return new Err<TResult, Exception>(ex);
            }
        }

        public static Result<TResult, Exception> Wrap<T1, T2, TResult>(in Func<T1, T2, TResult> f, in T1 arg1, in T2 arg2)
        {
            try
            {
                var value = f(arg1, arg2);
                if (value != null)
                    return new Ok<TResult, Exception>(value);
                return new Err<TResult, Exception>(new ArgumentException($"Calling {nameof(f)} has succeeded. But returned value is a null. Call with arg1: {arg1}, arg2: {arg2}."));
            }
            catch (Exception ex)
            {
                return new Err<TResult, Exception>(ex);
            }
        }

        public static Result<TResult, Exception> Wrap<T1, T2, T3, TResult>(in Func<T1, T2, T3, TResult> f, in T1 arg1, in T2 arg2, in T3 arg3)
        {
            try
            {
                var value = f(arg1, arg2, arg3);
                if (value != null)
                    return new Ok<TResult, Exception>(value);
                return new Err<TResult, Exception>(new ArgumentException($"Calling {nameof(f)} has succeeded. But returned value is a null. Call with arg1: {arg1}, arg2: {arg2}, arg3: {arg3}."));
            }
            catch (Exception ex)
            {
                return new Err<TResult, Exception>(ex);
            }
        }

        public static Result<TResult, Exception> Wrap<T1, T2, T3, T4, TResult>(in Func<T1, T2, T3, T4, TResult> f, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
        {
            try
            {
                var value = f(arg1, arg2, arg3, arg4);
                if (value != null)
                    return new Ok<TResult, Exception>(value);
                return new Err<TResult, Exception>(new ArgumentException($"Calling {nameof(f)} has succeeded. But returned value is a null. Call with arg1: {arg1}, arg2: {arg2}, arg3: {arg3}, arg4: {arg4}."));
            }
            catch (Exception ex)
            {
                return new Err<TResult, Exception>(ex);
            }
        }

        public static Result<TResult, Exception> Wrap<T1, T2, T3, T4, T5, TResult>(in Func<T1, T2, T3, T4, T5, TResult> f, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
        {
            try
            {
                var value = f(arg1, arg2, arg3, arg4, arg5);
                if (value != null)
                    return new Ok<TResult, Exception>(value);
                return new Err<TResult, Exception>(new ArgumentException($"Calling {nameof(f)} has succeeded. But returned value is a null. Call with arg1: {arg1}, arg2: {arg2}, arg3: {arg3}, arg4: {arg4}, arg5: {arg5}."));
            }
            catch (Exception ex)
            {
                return new Err<TResult, Exception>(ex);
            }
        }

        public static Result<TResult, ArgumentException> Wrap<TResult>(in TryPattern<TResult> f)
        {
            if (f(out TResult value))
                return new Ok<TResult, ArgumentException>(value);
            return new Err<TResult, ArgumentException>(new ArgumentException($"The `try pattern` returned a `false`. {nameof(f)} called."));
        }

        public static Result<TResult, ArgumentException> Wrap<T, TResult>(in TryPattern<T, TResult> f, in T arg)
        {
            if (f(arg, out TResult value))
                return new Ok<TResult, ArgumentException>(value);
            return new Err<TResult, ArgumentException>(new ArgumentException($"The `try pattern` returned a `false`. {nameof(f)} called with arg: {arg}."));
        }

        public static Result<TResult, ArgumentException> Wrap<T1, T2, TResult>(in TryPattern<T1, T2, TResult> f, in T1 arg1, in T2 arg2)
        {
            if (f(arg1, arg2, out TResult value))
                return new Ok<TResult, ArgumentException>(value);
            return new Err<TResult, ArgumentException>(new ArgumentException($"The `try pattern` returned a `false`. {nameof(f)} with arg1: {arg1}, arg2: {arg2}."));
        }

        public static Result<TResult, ArgumentException> Wrap<T1, T2, T3, TResult>(in TryPattern<T1, T2, T3, TResult> f, in T1 arg1, in T2 arg2, in T3 arg3)
        {
            if (f(arg1, arg2, arg3, out TResult value))
                return new Ok<TResult, ArgumentException>(value);
            return new Err<TResult, ArgumentException>(new ArgumentException($"The `try pattern` returned a `false`. {nameof(f)} with arg1: {arg1}, arg2: {arg2}, arg3: {arg3}."));
        }

        public static Result<TResult, ArgumentException> Wrap<T1, T2, T3, T4, TResult>(in TryPattern<T1, T2, T3, T4, TResult> f, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
        {
            if (f(arg1, arg2, arg3, arg4, out TResult value))
                return new Ok<TResult, ArgumentException>(value);
            return new Err<TResult, ArgumentException>(new ArgumentException($"The `try pattern` returned a `false`. {nameof(f)} with arg1: {arg1}, arg2: {arg2}, arg3: {arg3}, arg4: {arg4}."));
        }

        public static Result<TResult, ArgumentException> Wrap<T1, T2, T3, T4, T5, TResult>(in TryPattern<T1, T2, T3, T4, T5, TResult> f, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
        {
            if (f(arg1, arg2, arg3, arg4, arg5, out TResult value))
                return new Ok<TResult, ArgumentException>(value);
            return new Err<TResult, ArgumentException>(new ArgumentException($"The `try pattern` returned a `false`. {nameof(f)} with arg1: {arg1}, arg2: {arg2}, arg3: {arg3}, arg4: {arg4}, arg5: {arg5}."));
        }
    }
}
