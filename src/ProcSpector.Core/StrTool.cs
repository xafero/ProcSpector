using System;

namespace ProcSpector.Core
{
    public static class StrTool
    {
        public static string? TrimOrNull(this string? text)
        {
            var txt = text?.Trim();
            return string.IsNullOrWhiteSpace(txt) ? null : txt;
        }

        private const StringComparison Inv = StringComparison.InvariantCultureIgnoreCase;

        public static bool EqualsInv(this string? first, string? second)
        {
            var a = first?.TrimOrNull() ?? string.Empty;
            var b = second?.TrimOrNull() ?? string.Empty;
            return a.Equals(b, Inv);
        }
    }
}