using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProcSpector.API;

namespace ProcSpector.Impl.Net
{
    public sealed class NetPlatform : IPlatform, ISystem
    {
        public ISystem System => this;

        public IEnumerable<IProcess> GetAllProcesses()
        {
            var raw = Process.GetProcesses();
            foreach (var item in raw)
                if (WrapP(item) is { } wrap)
                    yield return wrap;
        }

        private static IProcess? WrapP(Process process)
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
            var modules = raw.Proc.Modules.Cast<ProcessModule>();
            foreach (var item in modules)
                if (WrapM(item) is { } wrap)
                    yield return wrap;
        }

        private static IModule WrapM(ProcessModule module)
        {
            var wrap = new StdMod(module);
            return wrap;
        }

        public IEnumerable<IMemRegion> GetRegions(IProcess proc)
        {
            return [];
        }

        public IEnumerable<IHandle> GetHandles(IProcess proc)
        {
            return [];
        }

        public string HostName => Environment.MachineName;
        public string UserName => Environment.UserName;
    }
}