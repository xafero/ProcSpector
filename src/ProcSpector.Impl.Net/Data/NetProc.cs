using System.Diagnostics;
using ProcSpector.API;

namespace ProcSpector.Impl.Net.Data
{
    public sealed class NetProc : IProcess
    {
        private readonly Process _item;

        public NetProc(Process item) => _item = item;

        public int? Id => _item.Id;
        public string Name => _item.ProcessName;

        public override string ToString() => $"#{Id} - {Name}";
    }
}