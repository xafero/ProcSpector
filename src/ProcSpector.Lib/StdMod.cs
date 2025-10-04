using System;
using System.Diagnostics;
using ByteSizeLib;
using static ProcSpector.Core.MiscExt;

namespace ProcSpector.Lib
{
    public sealed class StdMod : IModule
    {
        private readonly ProcessModule _module;

        public StdMod(ProcessModule module)
            => _module = module;

        public IntPtr BaseAddress => _module.BaseAddress;
        public string FileName => _module.FileName;
        public ByteSize Size => AsBytes(_module.ModuleMemorySize);

        public override string ToString()
            => $"({FileName})";
    }
}