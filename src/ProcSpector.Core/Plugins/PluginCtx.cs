using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

// ReSharper disable UnusedMember.Global

namespace ProcSpector.Core.Plugins
{
    public sealed class PluginCtx
    {
        public void LogDebug(object line)
        {
            Debug.WriteLine(line);
        }

        public IDictionary<CtxMenu, List<CtxMenuItem>> ContextMenu
            = new SortedDictionary<CtxMenu, List<CtxMenuItem>>();

        public void AddContextOption(string target, string title, EventHandler<string> handler)
        {
            var tgt = EnumTool.ParseArg<CtxMenu>(target).Single();
            if (!ContextMenu.TryGetValue(tgt, out var res))
                ContextMenu[tgt] = res = [];
            res.Add(new CtxMenuItem(Target: tgt, Title: title, Handler: handler));
        }
    }
}