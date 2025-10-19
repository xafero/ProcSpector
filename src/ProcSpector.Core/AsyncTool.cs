using System.Threading.Tasks;

namespace ProcSpector.Core
{
    public static class AsyncTool
    {
        public static T? GetVal<T>(this ValueTask<T>? tsk)
        {
            if (tsk is { } task)
                return task.IsCompleted ? task.Result : task.AsTask().Result;
            return default;
        }
    }
}