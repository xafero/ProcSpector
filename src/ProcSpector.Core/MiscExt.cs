using System;
using System.IO;
using ByteSizeLib;

namespace ProcSpector.Core
{
    public static class MiscExt
    {
        public static ByteSize AsBytes(long value)
        {
            return ByteSize.FromBytes(value);
        }

        public static TR? TryGet<TI, TR>(TI obj, Func<TI, TR> func)
        {
            try
            {
                return func.Invoke(obj);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static string GetTimedFileName(string prefix, string? middle, string ext)
        {
            var now = DateTime.Now;
            var nTx = $"{now:s}".Replace("T", " ").Replace(":", "");
            var title = StrTool.CleanCrazy(middle ?? "noTitle");
            var fileName = $"{prefix} {title} {nTx}.{ext}";
            return Path.Combine(Environment.CurrentDirectory, fileName);
        }
    }
}