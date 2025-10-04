using System.Collections.Generic;
using ProcSpector.API;

namespace ProcSpector.Lib
{
    public interface IProcessEx : IProcess
    {
        IEnumerable<IModule> Modules { get; }
    }
}