using System;
using Newtonsoft.Json;
using ProcSpector.Core.Plugins;

namespace ProcSpector
{
    internal static class Program
    {
        private static void Main()
        {
            var x = PluginTool.ListPlugins();
            var j = JsonConvert.SerializeObject(x);
            Console.WriteLine($"'{j}'");
            Console.ReadLine();
        }
    }
}