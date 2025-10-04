using System.Collections.Generic;
using ProcSpector.Lib.Memory;

namespace ProcSpector.Lib
{
    public interface ISystem
    {
        IEnumerable<IHandle> GetHandles(IProcess process);

        IEnumerable<IMemRegion> GetRegions(IProcess process);
    }
}