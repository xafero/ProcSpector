using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ProcSpector.OpenCV
{
    public static class MayTool
    {
        public static IDictionary<string, string>? ReadMem(this string? text)
        {
            if (text == null) return null;
            var dict = new SortedDictionary<string, string>();
            var lines = ToMemLines(text).ToArray();
            var count = -1;
            var step = -1;
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (count < 0)
                {
                    var next = lines[i + 1];
                    count = next.no - line.no;
                }
                if (step < 0)
                {
                    step = line.ctn.Take(3).Max(c => c.Length) / 2;
                }
                var hex = line.hex;
                var amount = count / step;
                var cnt = line.ctn.Take(amount);
                var val = string.Join("", cnt);
                dict.Add(hex, val);
            }
            return dict;
        }

        private static IEnumerable<(string hex, int no, string[] ctn)> ToMemLines(string text)
        {
            var lines = text.Split('\n');
            foreach (var line in lines)
            {
                var parts = line.Split(' ');
                if (parts.Length < 2) continue;
                var hex = parts[0].Trim();
                if (hex.Length != 8) continue;
                var nr = ParseHex(hex);
                var bp = parts.Skip(1).Select(x => x.Trim()).ToArray();
                if (bp.Length < 1) continue;
                yield return (hex, nr, bp);
            }
        }

        private static int ParseHex(string text)
        {
            return int.Parse(text, NumberStyles.HexNumber);
        }
    }
}