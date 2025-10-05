using System.Collections.Generic;
using System.Threading;
using ProcSpector.API;
using ProcSpector.Comm;

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
            ClientCore.WaitFor(new RequestMsg { Method = nameof(GetAllProcesses) });

            throw new System.NotImplementedException();
        }

        public IEnumerable<IModule> GetModules(IProcess proc)
        {
            ClientCore.WaitFor(new RequestMsg { Method = nameof(GetModules) });

            throw new System.NotImplementedException();
        }

        public IEnumerable<IMemRegion> GetRegions(IProcess proc)
        {
            ClientCore.WaitFor(new RequestMsg { Method = nameof(GetRegions) });

            throw new System.NotImplementedException();
        }

        public IEnumerable<IHandle> GetHandles(IProcess proc)
        {
            ClientCore.WaitFor(new RequestMsg { Method = nameof(GetHandles) });

            throw new System.NotImplementedException();
        }

        public string HostName
        {
            get
            {
                ClientCore.WaitFor(new RequestMsg { Method = nameof(HostName) });

                throw new System.NotImplementedException();
            }
        }

        public string UserName
        {
            get
            {
                ClientCore.WaitFor(new RequestMsg { Method = nameof(UserName) });

                throw new System.NotImplementedException();
            }
        }

        public void OpenFolder(IProcess proc)
        {
            ClientCore.WaitFor(new RequestMsg { Method = nameof(OpenFolder) });

            throw new System.NotImplementedException();
        }

        public void OpenFolder(IModule mod)
        {
            ClientCore.WaitFor(new RequestMsg { Method = nameof(OpenFolder) });

            throw new System.NotImplementedException();
        }

        public void Kill(IProcess proc)
        {
            ClientCore.WaitFor(new RequestMsg { Method = nameof(Kill) });

            throw new System.NotImplementedException();
        }

        public void CreateMemSave(IProcess proc)
        {
            ClientCore.WaitFor(new RequestMsg { Method = nameof(CreateMemSave) });

            throw new System.NotImplementedException();
        }

        public void CreateScreenShot(IProcess proc)
        {
            ClientCore.WaitFor(new RequestMsg { Method = nameof(CreateScreenShot) });

            throw new System.NotImplementedException();
        }

        public void CreateMiniDump(IProcess proc)
        {
            ClientCore.WaitFor(new RequestMsg { Method = nameof(CreateMiniDump) });

            throw new System.NotImplementedException();
        }

        public void Quit()
        {
            ClientCore.WaitFor(new RequestMsg { Method = nameof(Quit) });

            throw new System.NotImplementedException();
        }
    }
}