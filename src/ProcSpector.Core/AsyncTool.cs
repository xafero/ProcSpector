using System.Threading.Tasks;

namespace ProcSpector.Core
{
    public static class AsyncTool
    {
        public static T? GetVal<T>(this ValueTask<T>? tsk)
        {
            if (tsk is { } task)
                if (task.IsCompleted)
                    return task.Result;
                else
                    return task.ConfigureAwait(false).GetAwaiter().GetResult();
            return default;
        }

        public static T? GetVal<T>(this Task<T>? tsk)
        {
            if (tsk is { } task)
                if (task.IsCompleted)
                    return task.Result;
                else
                    return task.ConfigureAwait(false).GetAwaiter().GetResult();
            return default;
        }
    }
}