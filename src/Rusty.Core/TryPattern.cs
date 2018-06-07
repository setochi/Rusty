namespace Rusty.Core
{
    public delegate bool TryPattern<TResult>(out TResult value);
    public delegate bool TryPattern<T, TResult>(T arg, out TResult value);
    public delegate bool TryPattern<T1, T2, TResult>(T1 arg1, T2 arg2, out TResult value);
    public delegate bool TryPattern<T1, T2, T3, TResult>(T1 arg1, T2 arg2, T3 arg3, out TResult value);
    public delegate bool TryPattern<T1, T2, T3, T4, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, out TResult value);
    public delegate bool TryPattern<T1, T2, T3, T4, T5, TResult>(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, out TResult value);
}
