using System.Diagnostics;
using ProcSpector.API;
using System;
using ByteSizeLib;
using static ProcSpector.Core.MiscExt;

namespace ProcSpector.Impl.Net
{
    public sealed class StdMod : IModule
    {
        public ProcessModule Mod { get; }
        public ISystem Sys { get; }

        public StdMod(ProcessModule module, ISystem sys)
        {
            Mod = module;
            Sys = sys;
        }

        public IntPtr BaseAddress => Mod.BaseAddress;
        public string FileName => Mod.FileName;
        public ByteSize Size => AsBytes(Mod.ModuleMemorySize);

        public override string ToString()
            => $"({FileName})";

        // public void OpenFolder() => Sys.OpenFolder(this);
        long IModule.BaseAddress => Mod.BaseAddress;
        long IModule.Size => Mod.ModuleMemorySize;
    }
}