namespace ProcSpector.Core
{
    public static class StrTool
    {
        public static string? TrimOrNull(this string? text)
        {
            var txt = text?.Trim();
            return string.IsNullOrWhiteSpace(txt) ? null : txt;
        }
    }
}