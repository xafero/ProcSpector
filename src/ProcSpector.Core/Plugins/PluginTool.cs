using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Jint;

namespace ProcSpector.Core.Plugins
{
    public static class PluginTool
    {
        public static PluginCtx Context { get; } = new();

        private static Engine CreateEngine()
        {
            var opt = new Options();
            var engine = new Engine(opt);
            engine.SetValue("c", Context);
            return engine;
        }

        private static readonly Lazy<Engine> Engine = new(CreateEngine);

        private static IEnumerable<Plugin> ListPlugins(string folder = "Plugins")
        {
            const SearchOption o = SearchOption.AllDirectories;
            var path = Path.GetFullPath(folder);
            var files = Directory.GetFiles(path, "plugin.js", o);
            foreach (var file in files)
            {
                var local = Path.GetRelativePath(path, file);
                var dir = Path.GetDirectoryName(local);
                var root = Path.GetDirectoryName(file);
                var code = File.ReadAllText(file, Encoding.UTF8);
                var p = new Plugin { Name = dir, Root = root, Loaded = DateTime.Now };
                var ev = Engine.Value;
                ev.SetValue("plugin", p);
                ev.Execute(code, source: file);
                yield return p;
            }
        }

        private static Plugin[] CreateList() => ListPlugins().ToArray();
        public static readonly Lazy<Plugin[]> Plugins = new(CreateList);
    }
}