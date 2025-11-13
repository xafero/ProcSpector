using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;

namespace ProcSpector.OpenCV
{
    public static class RectTool
    {
        public static string GetText(this IReadOnlyCollection<OcrRect> matches,
            int ignoreY = 4, int ignoreX = 9, Func<string, string>? fix = null)
        {
            using var bld = new StringWriter();
            var y1 = matches.Select(m => m.Point.Y)
                .OrderBy(m => m).Distinct().ToArray();
            var y2 = y1.Skip(1).Zip(y1)
                .Where(x => x.First - x.Second <= ignoreY).ToArray();
            OcrRect? last = null;
            foreach (var one in matches.Select(m => (k: ToKey(m, y2), m))
                         .OrderBy(t => t.k).GroupBy(t => t.k))
            {
                var match = GetBestMatch(one);
                var letter = Path.GetFileNameWithoutExtension(match.File);
                string tmp;
                if (letter.StartsWith(tmp = "d_")) letter = letter[tmp.Length..];
                if (letter.StartsWith(tmp = "l_")) letter = letter[tmp.Length..];
                if (letter.StartsWith(tmp = "s_")) letter = letter[tmp.Length..];
                if (letter.StartsWith("o_eq")) letter = "=";
                if (letter.StartsWith("o_qm")) letter = "?";
                var line = $"{letter}";
                if (last != null && (match.Point.Y - last.Point.Y) >= ignoreY)
                    bld.WriteLine();
                if (last != null && (match.Point.X - last.Point.X) >= ignoreX)
                    bld.Write(" ");
                bld.Write(line);
                last = match;
            }
            var txt = bld.ToString();
            if (fix != null)
                txt = fix(txt);
            return txt;
        }

        private static string ToKey(OcrRect match, (int First, int Second)[]? fixes = null)
        {
            var y = match.Point.Y;
            if (fixes?.FirstOrDefault(f => f.First == y) is { } fix && fix.Second != 0)
                y = fix.Second;
            var x = match.Point.X;
            return $"{y:D4}:{x:D4}";
        }

        private static OcrRect GetBestMatch(IGrouping<string, (string k, OcrRect m)> group)
        {
            var all = group.ToArray();
            if (all.Length == 1)
                return all[0].m;
            var best = all.MaxBy(a => a.m.Size.Width * a.m.Size.Height);
            return best.m;
        }
    }
}