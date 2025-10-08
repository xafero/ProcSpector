using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ProcSpector.API;

namespace ProcSpector.Impl.Net
{
    public class NetPlatform : IPlatform, ISystem
    {
        public ISystem System => this;

        public Task<string> GetHostName()
        {
            var res = Environment.MachineName;
            return Task.FromResult(res);
        }

        public Task<string> GetUserName()
        {
            var res = Environment.UserName;
            return Task.FromResult(res);
        }

        private IProcess? WrapP(Process process)
        {
            try
            {
                _ = process.StartTime;
                _ = process.MainModule;
                _ = process.HandleCount;
            }
            catch (Exception)
            {
                return null;
            }
            var wrap = new StdProc(process, this);
            return wrap;
        }

        private IEnumerable<IProcess> GetAllProcessesInt()
        {
            var raw = Process.GetProcesses();
            foreach (var item in raw)
                if (WrapP(item) is { } wrap)
                    yield return wrap;
        }

        public IAsyncEnumerable<IProcess> GetAllProcesses()
        {
            var res = GetAllProcessesInt();
            return res.ToAsyncEnumerable();
        }

        private IModule WrapM(ProcessModule module)
        {
            var wrap = new StdMod(module, this);
            return wrap;
        }

        private IEnumerable<IModule> GetModulesInt(IProcess proc)
        {
            var raw = ProcExt.GetStdProc(proc, this);
            var modules = raw.Proc.Modules.Cast<ProcessModule>();
            foreach (var item in modules)
                if (WrapM(item) is { } wrap)
                    yield return wrap;
        }

        public IAsyncEnumerable<IModule> GetModules(IProcess proc)
        {
            var res = GetModulesInt(proc);
            return res.ToAsyncEnumerable();
        }

        public virtual IAsyncEnumerable<IMemRegion> GetRegions(IProcess proc)
        {
            var res = Enumerable.Empty<IMemRegion>();
            return res.ToAsyncEnumerable();
        }

        public virtual IAsyncEnumerable<IHandle> GetHandles(IProcess proc)
        {
            var res = Enumerable.Empty<IHandle>();
            return res.ToAsyncEnumerable();
        }

        public virtual Task<bool> CreateScreenShot(IProcess proc)
        {
            // NO-OP
            return Task.FromResult(false);
        }

        public virtual Task<bool> CreateScreenShot(IHandle handle)
        {
            // NO-OP
            return Task.FromResult(false);
        }

        public virtual Task<bool> CreateMemSave(IProcess proc)
        {
            // NO-OP
            return Task.FromResult(false);
        }

        public virtual Task<bool> CreateMemSave(IMemRegion mem)
        {
            // NO-OP
            return Task.FromResult(false);
        }

        public virtual Task<bool> CreateMiniDump(IProcess proc)
        {
            // NO-OP
            return Task.FromResult(false);
        }

        public virtual Task<bool> Quit()
        {
            // NO-OP
            return Task.FromResult(false);
        }

        public Task<bool> Kill(IProcess proc)
        {
            var res = ProcExt.Kill(proc, this);
            return Task.FromResult(res);
        }

        public Task<bool> OpenFolder(IProcess proc)
        {
            var res = ProcExt.OpenFolder(proc);
            return Task.FromResult(res);
        }

        public Task<bool> OpenFolder(IModule mod)
        {
            var res = ProcExt.OpenFolder(mod);
            return Task.FromResult(res);
        }
    }
}