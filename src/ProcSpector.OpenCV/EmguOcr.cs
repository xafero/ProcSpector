using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Emgu.CV;
using Emgu.CV.Structure;
using TM = Emgu.CV.CvEnum.TemplateMatchingType;

// ReSharper disable AccessToDisposedClosure

namespace ProcSpector.OpenCV
{
    public static class EmguOcr
    {
        public static IReadOnlyCollection<OcrRect> Find(string[] partFiles, string wholeFile,
            string root)
        {
            const double threshold = 0.9;
            using var source = Read(wholeFile);

            var res = new ConcurrentBag<OcrRect>();
            Parallel.ForEach(partFiles, partFile =>
                Read(threshold, source, res, partFile, root)
            );
            return res;
        }

        private static void Read(double threshold, Image<Bgr, byte> src,
            ConcurrentBag<OcrRect> res, string partFile, string root)
        {
            var local = Path.GetRelativePath(root, partFile);
            local = Path.GetFileNameWithoutExtension(local);

            using var template = Read(partFile);
            using var result = src.MatchTemplate(template, TM.CcoeffNormed);

            for (var y = 0; y < result.Rows; y++)
            for (var x = 0; x < result.Cols; x++)
            {
                var similarity = result.Data[y, x, 0];
                if (similarity >= threshold)
                    res.Add(new OcrRect(new Point(x, y), template.Size, similarity, local));
            }
        }

        private static Image<Bgr, byte> Read(string fileName)
        {
            return new Image<Bgr, byte>(fileName);
        }
    }
}