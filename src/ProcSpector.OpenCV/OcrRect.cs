using System.Drawing;

// ReSharper disable AccessToDisposedClosure

namespace ProcSpector.OpenCV
{
    public record OcrRect(
        Point Point,
        Size Size,
        float Similar,
        string File
    )
    {
        public Rectangle Rect => new(Point, Size);
    }
}