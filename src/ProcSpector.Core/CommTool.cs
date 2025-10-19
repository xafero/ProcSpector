using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProcSpector.API;

namespace ProcSpector.Core
{
    public static class CommTool
    {
        public static string GetUrl(this IClientCfg cfg)
        {
            return $"http://{cfg.Address}:{cfg.Port}";
        }

        private static readonly JsonSerializerSettings Cfg = new()
        {
            Converters = { new StringEnumConverter() },
            NullValueHandling = NullValueHandling.Ignore,
            ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            Formatting = Formatting.Indented
        };

        public static T? Unwrap<T>(this string? message)
        {
            if (message.TrimOrNull() is { } msg)
            {
                var val = JsonConvert.DeserializeObject<T>(msg, Cfg);
                return val;
            }
            return default;
        }

        public static string? Wrap(this object? raw)
        {
            if (raw is { } val)
            {
                var msg = JsonConvert.SerializeObject(val, Cfg);
                return msg;
            }
            return null;
        }
    }
}