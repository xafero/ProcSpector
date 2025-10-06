using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ProcSpector.API;

namespace ProcSpector.Impl.Net
{
    public class NetPlatform : IPlatform, ISystem
    {
        public ISystem System => this;

        public IEnumerable<IProcess> GetAllProcesses()
        {
            var raw = Process.GetProcesses();
            foreach (var item in raw)
                if (WrapP(item) is { } wrap)
                    yield return wrap;
        }

        private IProcess? WrapP(Process process)
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
            var wrap = new StdProc(process, this);
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

        private IModule WrapM(ProcessModule module)
        {
            var wrap = new StdMod(module, this);
            return wrap;
        }

        public virtual IEnumerable<IMemRegion> GetRegions(IProcess proc)
        {
            return [];
        }

        public virtual IEnumerable<IHandle> GetHandles(IProcess proc)
        {
            return [];
        }

        public string HostName => Environment.MachineName;
        public string UserName => Environment.UserName;

        public void OpenFolder(IProcess proc) => ProcExt.OpenFolder(proc);
        public void OpenFolder(IModule mod) => ProcExt.OpenFolder(mod);
        public bool Kill(IProcess proc) => ProcExt.Kill(proc);

        public virtual bool CreateMemSave(IProcess proc)
        {
            // NO-OP
            return false;
        }

        public virtual bool CreateScreenShot(IProcess proc)
        {
            // NO-OP
            return false;
        }

        public virtual bool CreateMiniDump(IProcess proc)
        {
            // NO-OP
            return false;
        }

        public void Quit()
        {
            // NO-OP
        }
    }
}