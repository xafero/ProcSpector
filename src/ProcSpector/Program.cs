using System;
using Newtonsoft.Json;
using ProcSpector.Core.Plugins;

namespace ProcSpector
{
    internal static class Program
    {
        private static void Main()
        {
            var x = PluginTool.Plugins.Value;
            var j = JsonConvert.SerializeObject(x);
            Console.WriteLine($"'{j}'");

            var z = PluginTool.Context.ContextMenu;
            foreach (var act in z[CtxMenu.Process])
            {
                Console.WriteLine("    " + act);
                act.Handler(42, "fine!");
            }

            Console.ReadLine();
        }
    }
}