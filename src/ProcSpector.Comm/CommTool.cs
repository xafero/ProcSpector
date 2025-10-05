using System;
using System.Text.Json;

namespace ProcSpector.Comm
{
    public static class CommTool
    {
        public static T? Unwrap<T>(this IMessage msg)
        {
            if (msg is ResponseMsg { Type: { } rType } rsp &&
                Type.GetType(rType) is { } tType)
            {
                object? val;
                if (rsp.Value is JsonElement je)
                    val = je.Deserialize(tType);
                else
                    val = Convert.ChangeType(rsp.Value, tType);
                if (val != null)
                    return (T)val;
            }
            return default;
        }
    }
}