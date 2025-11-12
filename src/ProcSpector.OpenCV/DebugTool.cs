using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace ProcSpector.OpenCV
{
    public static class DebugTool
    {
        public static void PaintIt(string file, IEnumerable<OcrRect> matches)
        {
            using var source = new Image<Bgr, byte>(file);
            using var imageToShow = source.Copy();
            var known = new Dictionary<string, Bgr>();
            var colors = new[]
            {
                Color.Red, Color.Green, Color.Blue, Color.Purple, Color.Yellow,
                Color.Fuchsia, Color.Brown, Color.Aqua, Color.Goldenrod,
                Color.YellowGreen, Color.CornflowerBlue, Color.Gray,
                Color.Crimson, Color.DarkOrange, Color.Indigo,
                Color.DarkSalmon, Color.PaleGoldenrod
            };
            foreach (var match in matches)
            {
                if (!known.TryGetValue(match.File, out var color))
                    known[match.File] = color = new Bgr(colors[known.Count]);
                imageToShow.Draw(match.Rect, color, 2);
            }
            var df = file.Replace(".png", ".d.png");
            imageToShow.Save(df);
        }
    }
}