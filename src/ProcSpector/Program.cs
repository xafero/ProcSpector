using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProcSpector.Core.Plugins;
using ProcSpector.Impl;

namespace ProcSpector
{
    internal static class Program
    {
        private static async Task Main()
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

            var p = Factory.Platform;
            await foreach (var process in p.Value.System.GetProcesses())
            {
                Console.WriteLine($" '{process}'");
            }

            Console.ReadLine();
        }
    }
}