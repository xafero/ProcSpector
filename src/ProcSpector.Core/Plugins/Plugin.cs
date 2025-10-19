using System;

namespace ProcSpector.Core.Plugins
{
    public sealed class Plugin
    {
        public string? Name { get; set; }
        public string? Root { get; set; }
        public DateTime? Loaded { get; set; }
    }
}