using System.Diagnostics;
using ProcSpector.API;

namespace ProcSpector.Impl.Net.Data
{
    public sealed class StdMod : IModule
    {
        private readonly ProcessModule _item;

        public StdMod(ProcessModule item) => _item = item;

        public long BaseAddress => _item.BaseAddress;
        public string FileName => _item.FileName;
        public long Size => _item.ModuleMemorySize;

        public override string ToString() => $"#{BaseAddress} - {FileName}";
    }
}