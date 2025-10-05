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
            => new RequestMsg { Method = nameof(GetAllProcesses) }.WaitFor().Unwrap<IProcess[]>() ?? [];

        public IEnumerable<IModule> GetModules(IProcess proc)
            => new RequestMsg { Method = nameof(GetModules) }.WaitFor().Unwrap<IModule[]>() ?? [];

        public IEnumerable<IMemRegion> GetRegions(IProcess proc)
            => new RequestMsg { Method = nameof(GetRegions) }.WaitFor().Unwrap<IMemRegion[]>() ?? [];

        public IEnumerable<IHandle> GetHandles(IProcess proc)
            => new RequestMsg { Method = nameof(GetHandles) }.WaitFor().Unwrap<IHandle[]>() ?? [];

        public string HostName
            => new RequestMsg { Method = nameof(HostName) }.WaitFor().Unwrap<string>() ?? "";

        public string UserName
            => new RequestMsg { Method = nameof(UserName) }.WaitFor().Unwrap<string>() ?? "";

        public void OpenFolder(IProcess proc)
            => new RequestMsg { Method = nameof(OpenFolder) }.WaitFor();

        public void OpenFolder(IModule mod)
            => new RequestMsg { Method = nameof(OpenFolder) }.WaitFor();

        public void Kill(IProcess proc)
            => new RequestMsg { Method = nameof(Kill) }.WaitFor();

        public void CreateMemSave(IProcess proc)
            => new RequestMsg { Method = nameof(CreateMemSave) }.WaitFor();

        public void CreateScreenShot(IProcess proc)
            => new RequestMsg { Method = nameof(CreateScreenShot) }.WaitFor();

        public void CreateMiniDump(IProcess proc)
            => new RequestMsg { Method = nameof(CreateMiniDump) }.WaitFor();

        public void Quit()
            => new RequestMsg { Method = nameof(Quit) }.WaitFor();
    }
}