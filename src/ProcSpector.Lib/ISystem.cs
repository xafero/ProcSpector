using System.Collections.Generic;
using ProcSpector.API;
using IMemRegion = ProcSpector.Lib.Memory.IMemRegion;

namespace ProcSpector.Lib
{
    public interface ISystem
    {
        IEnumerable<IHandle> GetHandles(IProcess process);

        IEnumerable<IMemRegion> GetRegions(IProcess process);
    }
}