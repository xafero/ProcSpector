using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProcSpector.API;
using ProcSpector.Core;

namespace ProcSpector.Comm
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
    }
}