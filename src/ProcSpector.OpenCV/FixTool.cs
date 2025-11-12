using System.Linq;

namespace ProcSpector.OpenCV
{
    public static class FixTool
    {
        public static string ForWine(string text)
        {
            var lines = text.Split('\n');
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                lines[i] = string.Join(" ", line.Replace(" ", "").Chunk(8).Select(c => new string(c)));
            }
            var result = string.Join('\n', lines);
            return result;
        }
    }
}