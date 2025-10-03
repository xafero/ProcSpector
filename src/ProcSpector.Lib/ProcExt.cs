using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

#pragma warning disable CA1416

namespace ProcSpector.Lib
{
    public static class ProcExt
    {
        public static void OpenFolder(IProcess proc) => OpenFileFolder(proc.FileName);
        public static void OpenFolder(IModule mod) => OpenFileFolder(mod.FileName);

        public static void OpenFileFolder(string? file)
        {
            var dir = Path.GetDirectoryName(file);
            OpenInShell(dir);
        }

        private static void OpenInShell(string? path)
        {
            var info = new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            };
            Process.Start(info);
        }

        public static void Kill(IProcess proc)
        {
            var real = ((StdProc)proc)._process;
            real.Kill(entireProcessTree: true);
        }

        private static IEnumerable<(WinStruct handle, int pixel)> GetBiggestWindows(IProcess proc)
        {
            return Win32.GetWindows()
                .Where(x => x.ProcessId == proc.Id)
                .Select(x => (x, p: GetPixelCount(Win32.GetWindowSize(x.WindowHandle))))
                .OrderByDescending(x => x.p);
        }

        private static WinStruct? GetMainWindow(IProcess proc)
        {
            var all = GetBiggestWindows(proc);
            var first = all.FirstOrDefault();
            return first.pixel != 0 ? first.handle : null;
        }

        public static void CreateScreenShot(IProcess proc)
        {
            var win = GetMainWindow(proc);
            if (win == null)
                return;
            CreateScreenShot(win.WindowHandle);
        }

        public static void CreateScreenShot(IHandle handle)
        {
            var win = handle.Handle;
            if (win == null)
                return;
            CreateScreenShot(win.Value);
        }

        private static string? CleanCrazy(string text)
        {
            return text
                .Replace("[", "").Replace("]", "")
                .Replace("(", "").Replace(")", "")
                .Replace("{", "").Replace("}", "")
                .Replace("-", "").Replace("+", "")
                .Replace(":", "").Replace(";", "")
                .Replace(@"\", "")
                .Replace("  ", " ")
                .TrimOrNull();
        }

        private static void CreateScreenShot(IntPtr hWnd)
        {
            var now = DateTime.Now;
            var nTx = $"{now:s}".Replace("T", " ").Replace(":", "");
            var title = CleanCrazy(Win32.GetWindowText(hWnd) ?? "noTitle");
            var fileName = $"Screenshot {title} {nTx}.png";
            var filePath = Path.Combine(Environment.CurrentDirectory, fileName);
            var format = ImageFormat.Png;
            using var bitmap = Win32.CaptureWindow(hWnd);
            bitmap?.Save(filePath, format);

            OpenInShell(filePath);
        }

        private static int GetPixelCount(Rectangle? size)
        {
            int pixels;
            if (size == null)
                pixels = 0;
            else
                pixels = size.Value.Width * size.Value.Height;
            return pixels;
        }
    }
}