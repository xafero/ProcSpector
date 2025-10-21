using System.Diagnostics;
using ProcSpector.API;
using ProcSpector.Impl.Net.Tools;

namespace ProcSpector.Impl.Net.Data
{
    public sealed class NetProc : IProcess
    {
        private readonly Process _item;

        public NetProc(Process item) => _item = item;

        public int? Id => _item.Id;
        public string Name => _item.ProcessName;
        public string? Path => _item.TryMainModule()?.FileName;

        public override string ToString() => $"#{Id} - {Name}";
    }
}