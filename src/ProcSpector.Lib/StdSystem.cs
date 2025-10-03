using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProcSpector.Lib.Memory;

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

        public IEnumerable<IMemRegion> GetRegions(IProcess proc)
        {
            var raw = (StdProc)proc;
            var real = raw._process;
            var regions = MemoryReader.ReadAllMemoryRegions(real);
            foreach (var item in regions)
                if (WrapR(item) is { } wrap)
                    yield return wrap;
        }

        private static IMemRegion WrapR(MemoryRegion region)
        {
            var wrap = new StdMem(region);
            return wrap;
        }

        private static IEnumerable<WinStruct> GetAllHandles(IProcess proc)
        {
            foreach (var top in Win32.GetWindows().Where(w => w.ProcessId == proc.Id))
            {
                yield return top;

                foreach (var sub in Win32.GetWindows(top.WindowHandle))
                    yield return sub;
            }
        }

        public IEnumerable<IHandle> GetHandles(IProcess proc)
        {
            var res = GetAllHandles(proc).Select(WrapH)
                .Where(x =>
                {
                    var p = (StdWnd)x;
                    return p.ProcessId == proc.Id && p.Title != null;
                });
            return res;
        }

        private static IHandle WrapH(WinStruct obj)
        {
            var wrap = new StdWnd(obj.WindowHandle, obj.ProcessId, obj.ThreadId, obj.ParentHandle);
            return wrap;
        }
    }
}