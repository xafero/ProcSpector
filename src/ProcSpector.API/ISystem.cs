using System.Collections.Generic;

namespace ProcSpector.API
{
    public interface ISystem
    {
        FeatureFlags Flags { get; }

        IAsyncEnumerable<IProcess> GetProcesses();
    }
}