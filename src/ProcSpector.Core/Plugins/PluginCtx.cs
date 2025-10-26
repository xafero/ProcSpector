using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using ProcSpector.API;
using ProcSpector.OpenCV;

// ReSharper disable UnusedMember.Global

namespace ProcSpector.Core.Plugins
{
    public sealed class PluginCtx
    {
        public void LogDebug(object line)
        {
            if (!Debugger.IsAttached || !RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                Console.WriteLine(line);
            else
                Debug.WriteLine(line);
        }

        public IDictionary<CtxMenu, List<CtxMenuItem>> ContextMenu
            = new SortedDictionary<CtxMenu, List<CtxMenuItem>>();

        public void AddContextOption(string target, string title, EventHandler<object> handler)
        {
            var tgt = EnumTool.ParseArg<CtxMenu>(target).Single();
            if (!ContextMenu.TryGetValue(tgt, out var res))
                ContextMenu[tgt] = res = [];
            res.Add(new CtxMenuItem(Target: tgt, Title: title, Handler: handler));
        }

        public IPlatform? P { get; set; }
        public ISystem1? S1 => P?.System1;
        public ISystem2? S2 => P?.System2;

        public IProcess? FindProcess(string name)
            => (S1?.GetProcesses().FirstOrDefaultAsync(x => x.Name.EqualsInv(name))).GetVal();

        public IHandle? FindWindow(IProcess p, string name)
            => (S2?.GetHandles(p).FirstOrDefaultAsync(x => x.Title.EqualsInv(name))).GetVal();

        public IFile? ScreenShot(IHandle h)
            => S2?.CreateScreenShot(h).GetVal();

        public void DoOcr(IFile file, string root, string sub)
        {
            var dir = Path.Combine(root, sub);
            var files = FileExt.List(dir, "*.png");
            using var tmp = FileExt.CreateTmp();
            tmp.SetBytes(file.Bytes);
            var txt = EmguOcr.Find(files, tmp.FullPath, dir).GetText();
            var dict = txt.ReadMem();
            if (dict == null) return;
            const string jff = "mem.json";
            using (var jf = File.CreateText(jff))
                jf.WriteLine(dict.Wrap());
            FileExt.OpenInShell(jff);
        }
    }
}