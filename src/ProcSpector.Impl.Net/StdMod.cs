using System.Diagnostics;
using ProcSpector.API;

namespace ProcSpector.Impl.Net
{
    public sealed class StdMod : IModule
    {
        public ProcessModule Mod { get; }

        public StdMod(ProcessModule module)
        {
            Mod = module;
        }

        public void OpenFolder()
        {
            throw new System.NotImplementedException();
        }
    }
}