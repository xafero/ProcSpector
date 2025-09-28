using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProcSpector.Lib
{
    public sealed class StdSystem : ISystem
    {
        public IEnumerable<IProcess> Processes
        {
            get
            {
                var raw = Process.GetProcesses();
                foreach (var item in raw)
                    if (Wrap(item) is { } wrap)
                        yield return wrap;
            }
        }

        private static IProcess? Wrap(Process process)
        {
            try
            {
                _ = process.StartTime;
            }
            catch (Exception ex)
            {
                return null;
            }
            var wrap = new StdProc(process);
            return wrap;
        }
    }
}