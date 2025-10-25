using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using ProcSpector.API;
using ProcSpector.Core;
using ProcSpector.Impl.Net.Data;
using ProcSpector.Impl.Net.Tools;
using ProcSpector.Impl.Win.Data;
using ProcSpector.Impl.Win.Memory;

#pragma warning disable CA1416

namespace ProcSpector.Impl.Win.Internal
{
    public static class Win32Ext
    {
        public static IFile CreateMemSave(IProcess proc)
        {
            var title = Path.GetFileNameWithoutExtension(proc.Path);
            var filePath = MiscExt.GetTimedFileName("RawMem", title, "bin");

            var real = ProcExt.GetStdProc(proc).GetReal();
            var regions = MemoryReader.ReadAllMemoryRegions(real);

            var bunch = StdFile.Create(filePath, null, stream =>
            {
                foreach (var region in regions)
                    stream.Write(region.Data);
            });
            return bunch;
        }

        public static IFile? CreateMemSave(IMemRegion region)
        {
            var title = $"0x{region.BaseAddress:X}";
            var filePath = MiscExt.GetTimedFileName("Region", title, "bin");

            var real = ((StdMem)region).GetReal();
            if (real.Data is not { Length: >= 1 } data)
                return null;

            var bunch = StdFile.Create(filePath, data, null);
            return bunch;
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

        public static IFile? CreateScreenShot(IProcess proc)
        {
            var win = GetMainWindow(proc);
            if (win == null)
                return null;
            return CreateScreenShot(win.WindowHandle);
        }

        public static IFile? CreateScreenShot(IHandle handle)
        {
            var win = handle.Handle;
            if (win == null)
                return null;
            return CreateScreenShot(new IntPtr(win.Value));
        }

        private static IFile CreateScreenShot(IntPtr hWnd)
        {
            var title = Win32.GetWindowText(hWnd);
            var filePath = MiscExt.GetTimedFileName("Screenshot", title, "png");

            var bunch = StdFile.Create(filePath, null, stream =>
            {
                var format = ImageFormat.Png;
                using var bitmap = Win32Gdi.CaptureWindow(hWnd);
                bitmap?.Save(stream, format);
            });
            return bunch;
        }

        public static IFile CreateMiniDump(IProcess proc, WinPlatform winPlatform)
        {
            var title = Path.GetFileNameWithoutExtension(proc.Path);
            var filePath = MiscExt.GetTimedFileName("MiniDump", title, "dmp");

            var real = ProcExt.GetStdProc(proc).GetReal();
            var bytes = MiniDumper.CreateDump(real);

            var bunch = StdFile.Create(filePath, bytes, null);
            return bunch;
        }
    }
}