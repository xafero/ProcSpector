using System.Collections.Generic;
using ProcSpector.Lib.Memory;

namespace ProcSpector.Lib
{
    public interface ISystem
    {
        IEnumerable<IProcess> GetAllProcesses();

        IEnumerable<IModule> GetModules(IProcess process);

        IEnumerable<IHandle> GetHandles(IProcess process);

        IEnumerable<IMemRegion> GetRegions(IProcess process);

        string UserName { get; }

        string HostName { get; }
    }
}