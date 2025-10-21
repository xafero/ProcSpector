using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProcSpector.API;

// ReSharper disable UnusedMember.Global

namespace ProcSpector.Core.Plugins
{
    public sealed class PluginCtx
    {
        public void LogDebug(object line)
        {
            if (!Debugger.IsAttached)
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

        public ISystem? S { get; set; }

        public IProcess? GetFirstProcess(string name)
            => (S?.GetProcesses().FirstOrDefaultAsync(x => x.Name.EqualsInv(name))).GetVal();
    }
}