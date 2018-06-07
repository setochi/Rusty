using System;

namespace Rusty.Core
{
    /// <summary>
    /// Option Operator
    /// </summary>
    public static class Option
    {
        public static Option<T> Wrap<T>(in T? v) where T : struct
        {
            if (v.HasValue)
                return new Some<T>(v.Value);
            return None<T>.Instance;
        }

        public static Option<T> Wrap<T>(in T v) where T : class
        {
            if (v == null)
                return None<T>.Instance;
            return new Some<T>(v);
        }

        public static Option<TResult> Wrap<TResult>(in Func<TResult> f)
        {
            try
            {
                var value = f();
                if (value != null)
                    return new Some<TResult>(value);
            }
            catch { }

            return None<TResult>.Instance;
        }

        public static Option<TResult> Wrap<T1, TResult>(in Func<T1, TResult> f, in T1 arg)
        {
            try
            {
                var value = f(arg);
                if (value != null)
                    return new Some<TResult>(value);
            }
            catch { }

            return None<TResult>.Instance;
        }

        public static Option<TResult> Wrap<T1, T2, TResult>(in Func<T1, T2, TResult> f, in T1 arg1, T2 arg2)
        {
            try
            {
                var value = f(arg1, arg2);
                if (value != null)
                    return new Some<TResult>(value);
            }
            catch { }

            return None<TResult>.Instance;
        }

        public static Option<TResult> Wrap<T1, T2, T3, TResult>(in Func<T1, T2, T3, TResult> f, in T1 arg1, in T2 arg2, in T3 arg3)
        {
            try
            {
                var value = f(arg1, arg2, arg3);
                if (value != null)
                    return new Some<TResult>(value);
            }
            catch { }

            return None<TResult>.Instance;
        }

        public static Option<TResult> Wrap<T1, T2, T3, T4, TResult>(in Func<T1, T2, T3, T4, TResult> f, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
        {
            try
            {
                var value = f(arg1, arg2, arg3, arg4);
                if (value != null)
                    return new Some<TResult>(value);
            }
            catch { }

            return None<TResult>.Instance;
        }

        public static Option<TResult> Wrap<T1, T2, T3, T4, T5, TResult>(in Func<T1, T2, T3, T4, T5, TResult> f, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
        {
            try
            {
                var value = f(arg1, arg2, arg3, arg4, arg5);
                if (value != null)
                    return new Some<TResult>(value);
            }
            catch { }

            return None<TResult>.Instance;
        }

        public static Option<TResult> Wrap<TResult>(in TryPattern<TResult> f)
        {
            if (f(out TResult value))
                return new Some<TResult>(value);
            return None<TResult>.Instance;
        }

        public static Option<TResult> Wrap<T, TResult>(in TryPattern<T, TResult> f, in T arg)
        {
            if (f(arg, out TResult value))
                return new Some<TResult>(value);
            return None<TResult>.Instance;
        }

        public static Option<TResult> Wrap<T1, T2, TResult>(in TryPattern<T1, T2, TResult> f, in T1 arg1, in T2 arg2)
        {
            if (f(arg1, arg2, out TResult value))
                return new Some<TResult>(value);
            return None<TResult>.Instance;
        }

        public static Option<TResult> Wrap<T1, T2, T3, TResult>(in TryPattern<T1, T2, T3, TResult> f, in T1 arg1, in T2 arg2, in T3 arg3)
        {
            if (f(arg1, arg2, arg3, out TResult value))
                return new Some<TResult>(value);
            return None<TResult>.Instance;
        }

        public static Option<TResult> Wrap<T1, T2, T3, T4, TResult>(in TryPattern<T1, T2, T3, T4, TResult> f, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4)
        {
            if (f(arg1, arg2, arg3, arg4, out TResult value))
                return new Some<TResult>(value);
            return None<TResult>.Instance;
        }

        public static Option<TResult> Wrap<T1, T2, T3, T4, T5, TResult>(in TryPattern<T1, T2, T3, T4, T5, TResult> f, in T1 arg1, in T2 arg2, in T3 arg3, in T4 arg4, in T5 arg5)
        {
            if (f(arg1, arg2, arg3, arg4, arg5, out TResult value))
                return new Some<TResult>(value);
            return None<TResult>.Instance;
        }
    }
}
