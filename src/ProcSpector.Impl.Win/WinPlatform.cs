using System.Collections.Generic;
using ProcSpector.API;

namespace ProcSpector.Impl.Win
{
    public sealed class WinPlatform : IPlatform, ISystem
    {
        public ISystem System => this;

        public IEnumerable<IProcess> GetAllProcesses()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IModule> GetModules(IProcess proc)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IMemRegion> GetRegions(IProcess proc)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IHandle> GetHandles(IProcess proc)
        {
            throw new System.NotImplementedException();
        }

        public string HostName { get; }
        public string UserName { get; }
    }
}