using System.Collections.Generic;

namespace ProcSpector.Lib
{
    public interface ISystem
    {
        IEnumerable<IProcess> GetAllProcesses();

        IEnumerable<IModule> GetModules(IProcess process);

        string UserName { get; }

        string HostName { get; }
    }
}