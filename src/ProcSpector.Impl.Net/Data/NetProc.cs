using System.Diagnostics;
using ProcSpector.API;

namespace ProcSpector.Impl.Net
{
    public sealed class NetProc : IProcess
    {
        private readonly Process _item;

        public NetProc(Process item) => _item = item;

        public string Name => _item.ProcessName;

        public override string ToString() => $"{Name}";
    }
}