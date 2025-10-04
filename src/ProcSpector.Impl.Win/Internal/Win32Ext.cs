using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ProcSpector.API;
using ProcSpector.Core;
using ProcSpector.Impl.Net;
using System;
using System.Drawing.Imaging;
using ProcSpector.Lib.Memory;

#pragma warning disable CA1416

namespace ProcSpector.Lib
{
    public static class Win32Ext
    {
        public static WinStruct? GetMainWindow(IProcess proc)
        {
            var all = GetBiggestWindows(proc);
            var first = all.FirstOrDefault();
            return first.pixel != 0 ? first.handle : null;
        }

        private static IEnumerable<(WinStruct handle, int pixel)> GetBiggestWindows(IProcess proc)
        {
            return Win32.GetWindows()
                .Where(x => x.ProcessId == proc.Id)
                .Select(x => (x, p: GetPixelCount(Win32.GetWindowSize(x.WindowHandle))))
                .OrderByDescending(x => x.p);
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

        public static void CreateMemSave(IMemRegion region)
        {
            var title = $"0x{region.BaseAddress.ToInt64():X}";
            var filePath = MiscExt.GetTimedFileName("Region", title, "bin");

            var real = ((StdMem)region).Mem;
            if (real.Data is not { Length: >= 1 } data)
                return;

            File.WriteAllBytes(filePath, data);
            ProcExt.OpenInShell(filePath);
        }

        public static void CreateScreenShot(IProcess proc)
        {
            var win = Win32Ext.GetMainWindow(proc);
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

        private static void CreateScreenShot(IntPtr hWnd)
        {
            var title = Win32.GetWindowText(hWnd);
            var filePath = MiscExt.GetTimedFileName("Screenshot", title, "png");

            var format = ImageFormat.Png;
            using (var bitmap = Win32Gdi.CaptureWindow(hWnd))
                bitmap?.Save(filePath, format);

            ProcExt.OpenInShell(filePath);
        }

        public static void CreateMiniDump(IProcess proc)
        {
            var title = Path.GetFileNameWithoutExtension(proc.FileName);
            var filePath = MiscExt.GetTimedFileName("MiniDump", title, "dmp");

            var real = ((StdProc)proc).Proc;
            MiniDumper.CreateDump(real, filePath);

            ProcExt.OpenInShell(filePath);
        }

        public static void CreateMemSave(IProcess proc)
        {
            var title = Path.GetFileNameWithoutExtension(proc.FileName);
            var filePath = MiscExt.GetTimedFileName("RawMem", title, "bin");

            var real = ((StdProc)proc).Proc;
            var regions = MemoryReader.ReadAllMemoryRegions(real);

            using (var stream = File.Create(filePath))
                foreach (var region in regions)
                    stream.Write(region.Data);

            ProcExt.OpenInShell(filePath);
        }
    }
}