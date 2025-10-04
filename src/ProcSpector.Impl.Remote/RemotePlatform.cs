using System.Collections.Generic;
using System.Threading;
using ProcSpector.API;

namespace ProcSpector.Impl.Remote
{
    public sealed class RemotePlatform : IPlatform, ISystem
    {
        internal IClientCfg Cfg { get; }

        public RemotePlatform(IClientCfg cfg)
        {
            Cfg = cfg;

            var thread = new Thread(ClientCore.StartLoop) { IsBackground = true, Name = "Loop" };
            thread.Start(this);
        }

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

        public void OpenFolder(IProcess proc)
        {
            throw new System.NotImplementedException();
        }

        public void OpenFolder(IModule mod)
        {
            throw new System.NotImplementedException();
        }

        public void Kill(IProcess proc)
        {
            throw new System.NotImplementedException();
        }

        public void CreateMemSave(IProcess proc)
        {
            throw new System.NotImplementedException();
        }

        public void CreateScreenShot(IProcess proc)
        {
            throw new System.NotImplementedException();
        }

        public void CreateMiniDump(IProcess proc)
        {
            throw new System.NotImplementedException();
        }
    }
}