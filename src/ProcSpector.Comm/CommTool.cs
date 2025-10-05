using System.Text.Json;
using ProcSpector.Core;

namespace ProcSpector.Comm
{
    public static class CommTool
    {
        public static T? Unwrap<T>(this string message)
        {
            if (message.TrimOrNull() is { } msg)
            {
                var val = JsonSerializer.Deserialize<T>(msg);
                return val;
            }
            return default;
        }
    }
}