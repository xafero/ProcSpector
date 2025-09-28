using System.Diagnostics;
using System.Linq;

namespace ProcSpector.Lib
{
    public sealed class StdSystem : ISystem
    {
        public IProcess[] Processes
        {
            get
            {
                var raw = Process.GetProcesses();
                var it = raw.Select(IProcess (x) => new StdProc(x));
                return it.ToArray();
            }
        }
    }
}