using System.Collections.Generic;

namespace ProcSpector.API
{
    public interface ISystem
    {
        IEnumerable<IProcess> GetAllProcesses();
        IEnumerable<IModule> GetModules(IProcess proc);
        IEnumerable<IMemRegion> GetRegions(IProcess proc);
        IEnumerable<IHandle> GetHandles(IProcess proc);

        string HostName { get; }
        string UserName { get; }
    }
}