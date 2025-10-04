using System;
using System.Drawing.Imaging;
using System.IO;
using ProcSpector.API;
using ProcSpector.Core;
using ProcSpector.Impl.Net;
using ProcSpector.Lib.Memory;

#pragma warning disable CA1416

namespace ProcSpector.Lib
{
    public static class ProcExt2
    {
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