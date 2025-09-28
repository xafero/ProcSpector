using System.Diagnostics;

namespace ProcSpector.Lib
{
    public sealed class StdProc : IProcess
    {
        private readonly Process _process;

        public StdProc(Process process)
            => _process = process;

        public string ProcessName
            => _process.ProcessName;

        public override string ToString()
            => $"({ProcessName})";
    }
}