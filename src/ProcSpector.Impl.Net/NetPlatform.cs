using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProcSpector.API;
using ProcSpector.Impl.Net.Data;

namespace ProcSpector.Impl.Net
{
    public class NetPlatform : IPlatform, ISystem
    {
        public ISystem System => this;

        public FeatureFlags Flags => FeatureFlags.GetProcesses;

        public IAsyncEnumerable<IProcess> GetProcesses()
            => GetProcessesSync().ToAsyncEnumerable();

        private IEnumerable<IProcess> GetProcessesSync()
        {
            foreach (var item in Process.GetProcesses())
                yield return new NetProc(item);
        }
    }
}