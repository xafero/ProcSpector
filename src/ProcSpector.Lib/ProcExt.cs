using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using ProcSpector.API;
using ProcSpector.Core;
using ProcSpector.Impl.Net;
using ProcSpector.Lib.Memory;
using IMemRegion = ProcSpector.Lib.Memory.IMemRegion;

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
            var real = ((StdProc)proc).Proc;
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
            var title = Win32.GetWindowText(hWnd);
            var filePath = GetTimedFileName("Screenshot", title, "png");

            var format = ImageFormat.Png;
            using (var bitmap = Win32.CaptureWindow(hWnd))
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

        public static void CreateMiniDump(IProcess proc)
        {
            var title = Path.GetFileNameWithoutExtension(proc.FileName);
            var filePath = GetTimedFileName("MiniDump", title, "dmp");

            var real = ((StdProc)proc).Proc;
            MiniDumper.CreateDump(real, filePath);

            OpenInShell(filePath);
        }

        private static string GetTimedFileName(string prefix, string? middle, string ext)
        {
            var now = DateTime.Now;
            var nTx = $"{now:s}".Replace("T", " ").Replace(":", "");
            var title = CleanCrazy(middle ?? "noTitle");
            var fileName = $"{prefix} {title} {nTx}.{ext}";
            return Path.Combine(Environment.CurrentDirectory, fileName);
        }

        public static void CreateMemSave(IMemRegion region)
        {
            var title = $"0x{region.BaseAddress.ToInt64():X}";
            var filePath = GetTimedFileName("Region", title, "bin");

            var real = ((StdMem)region)._mem;
            if (real.Data is not { Length: >= 1 } data)
                return;

            File.WriteAllBytes(filePath, data);
            OpenInShell(filePath);
        }

        public static void CreateMemSave(IProcess proc)
        {
            var title = Path.GetFileNameWithoutExtension(proc.FileName);
            var filePath = GetTimedFileName("RawMem", title, "bin");

            var real = ((StdProc)proc).Proc;
            var regions = MemoryReader.ReadAllMemoryRegions(real);

            using (var stream = File.Create(filePath))
                foreach (var region in regions)
                    stream.Write(region.Data);

            OpenInShell(filePath);
        }
    }
}