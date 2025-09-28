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
            var modules = raw._process.Modules.Cast<ProcessModule>();
            foreach (var item in modules)
                if (WrapM(item) is { } wrap)
                    yield return wrap;
        }

        private static IModule WrapM(ProcessModule module)
        {
            var wrap = new StdMod(module);
            return wrap;
        }

        public IEnumerable<IHandle> GetHandles(IProcess proc)
        {
            var raw = (StdProc)proc;
            var res = Win32.GetWindows()
                .Select(h => WrapH(h));
            return res;
        }

        private static IHandle WrapH(WinStruct obj)
        {
            var wrap = new StdWnd(obj.MainWindowHandle, obj.ProcessId, obj.ThreadId);
            return wrap;
        }
    }
}