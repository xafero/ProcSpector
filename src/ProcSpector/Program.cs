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
            var s = Factory.Platform.Value.System;
            PluginTool.Context.S = s;

            var x = PluginTool.Plugins.Value;
            var j = JsonConvert.SerializeObject(x);
            Console.WriteLine($"'{j}'");

            var z = PluginTool.Context.ContextMenu;
            foreach (var act in z[CtxMenu.Process])
            {
                Console.WriteLine("    " + act);
                act.Handler(42, "fine!");
            }

            /* await foreach (var process in s.GetProcesses())
            {
                Console.WriteLine($" '{process}'");
            } */

            Console.ReadLine();
        }
    }
}