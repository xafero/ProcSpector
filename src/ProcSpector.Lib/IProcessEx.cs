using System.Collections.Generic;

namespace ProcSpector.Lib
{
    public interface IProcessEx : IProcess
    {
        IEnumerable<IModule> Modules { get; }
    }
}