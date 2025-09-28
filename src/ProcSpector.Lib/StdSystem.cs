using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ProcSpector.Lib
{
    public sealed class StdSystem : ISystem
    {
        public string UserName => Environment.UserName;
        public string HostName => Environment.MachineName;

        public IEnumerable<IProcess> GetAllProcesses()
        {
            var raw = Process.GetProcesses();
            foreach (var item in raw)
                if (Wrap(item) is { } wrap)
                    yield return wrap;
        }

        private static IProcess? Wrap(Process process)
        {
            try
            {
                _ = process.StartTime;
                _ = process.MainModule;
            }
            catch (Exception)
            {
                return null;
            }
            var wrap = new StdProc(process);
            return wrap;
        }

        public IEnumerable<IModule> GetModules(IProcess proc)
        {
            var raw = (StdProc)proc;
            var modules = raw._process.Modules.Cast<ProcessModule>();
            foreach (var item in modules)
                if (Wrap(item) is { } wrap)
                    yield return wrap;
        }

        private static IModule Wrap(ProcessModule module)
        {
            var wrap = new StdMod(module);
            return wrap;
        }
    }
}