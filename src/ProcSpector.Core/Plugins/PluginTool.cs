using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Jint;

namespace ProcSpector.Core.Plugins
{
    public static class PluginTool
    {
        private static Engine CreateEngine()
        {
            var opt = new Options();
            var engine = new Engine(opt);
            engine.SetValue("c", new PluginCtx());
            return engine;
        }

        private static readonly Lazy<Engine> Engine = new(CreateEngine);

        public static IEnumerable<Plugin> ListPlugins(string folder = "Plugins")
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
                Engine.Value.Execute(code, source: file);
                yield return new Plugin { Name = dir, Root = root };
            }
        }
    }
}