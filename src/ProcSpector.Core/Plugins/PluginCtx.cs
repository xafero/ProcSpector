using System.Diagnostics;

// ReSharper disable UnusedMember.Global

namespace ProcSpector.Core.Plugins
{
    public sealed class PluginCtx
    {
        public void LogDebug(object line)
        {
            Debug.WriteLine(line);
        }
    }
}