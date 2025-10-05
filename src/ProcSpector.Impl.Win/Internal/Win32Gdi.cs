using System;
using System.Drawing;
using System.Runtime.InteropServices;

#pragma warning disable CA1416

namespace ProcSpector.Impl.Win.Internal
{
    public static class Win32Gdi
    {
        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        
        [DllImport("gdi32")]
        private static extern IntPtr CreateCompatibleBitmap(IntPtr hDc, int nWidth, int nHeight);
        
        [DllImport("user32")]
        private static extern IntPtr GetWindowDC(IntPtr hWnd);
        
        [DllImport("gdi32")]
        private static extern IntPtr CreateCompatibleDC(IntPtr hDc);
        
        [DllImport("gdi32")]
        private static extern IntPtr SelectObject(IntPtr hDc, IntPtr hGdiObj);
        
        [DllImport("gdi32")]
        private static extern bool DeleteDC(IntPtr hDc);

        [DllImport("gdi32")]
        private static extern bool DeleteObject(IntPtr hObject);
        
        [DllImport("user32")]
        private static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);
        
        [DllImport("gdi32")]
        private static extern bool BitBlt(IntPtr hDestDc, int x, int y, int nWidth, int nHeight,
            IntPtr hSrcDc, int xSrc, int ySrc, uint dwRop);
        
        private const uint SrcCopy = 0x00CC0020;

        public static Bitmap? CaptureWindow(IntPtr hWnd)
        {
            if (!Win32.GetWindowRect(hWnd, out var windowRect))
                return null;

            var width = windowRect.Width;
            var height = windowRect.Height;

            if (width <= 0 || height <= 0)
                return null;

            var windowDc = GetWindowDC(hWnd);
            if (windowDc == IntPtr.Zero)
                return null;

            try
            {
                var memoryDc = CreateCompatibleDC(windowDc);
                var bitmap = CreateCompatibleBitmap(windowDc, width, height);
                var oldBitmap = SelectObject(memoryDc, bitmap);

                BitBlt(memoryDc, 0, 0, width, height, windowDc, 0, 0, SrcCopy);

                var result = Image.FromHbitmap(bitmap);

                SelectObject(memoryDc, oldBitmap);
                DeleteObject(bitmap);
                DeleteDC(memoryDc);

                return result;
            }
            finally
            {
                ReleaseDC(hWnd, windowDc);
            }
        }
    }
}