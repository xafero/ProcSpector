using System.Collections.Generic;

namespace ProcSpector.Lib
{
    public interface ISystem
    {
        IEnumerable<IProcess> Processes { get; }
    }
}