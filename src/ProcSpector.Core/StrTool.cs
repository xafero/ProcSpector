using System;
using System.Text;

namespace ProcSpector.Core
{
    public static class StrTool
    {
        public static readonly Encoding Enc = Encoding.UTF8;
        public const StringComparison Inv = StringComparison.InvariantCultureIgnoreCase;

        public static string? TrimOrNull(this string? text)
        {
            var txt = text?.Trim();
            return string.IsNullOrWhiteSpace(txt) ? null : txt;
        }
    }
}