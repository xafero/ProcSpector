using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ProcSpector.API;
using ProcSpector.Impl.Net.Data;
using ProcSpector.Impl.Net.Tools;

namespace ProcSpector.Impl.Net
{
    public class NetPlatform : IPlatform, ISystem1, ISystem3
    {
        public ISystem1 System1 => this;
        public virtual ISystem2? System2 => null;
        public ISystem3 System3 => this;

        public Task<IUserInfo?> GetUserInfo()
            => Task.FromResult<IUserInfo?>(GetUserInfoSync());

        private IUserInfo GetUserInfoSync()
            => new NetUser();

        public IAsyncEnumerable<IProcess> GetProcesses()
            => GetProcessesSync().ToAsyncEnumerable();

        private IEnumerable<IProcess> GetProcessesSync()
        {
            foreach (var item in Process.GetProcesses())
                if (WrapP(item) is { } wrap)
                    yield return wrap;
        }

        private static StdProc? WrapP(Process process)
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
            var wrap = new StdProc(process);
            return wrap;
        }

        public IAsyncEnumerable<IModule> GetModules(IProcess proc)
            => GetModulesSync(proc).ToAsyncEnumerable();

        private IEnumerable<IModule> GetModulesSync(IProcess proc)
        {
            var raw = ProcExt.GetStdProc(proc).GetReal();
            var modules = raw.Modules.Cast<ProcessModule>();
            foreach (var item in modules)
                if (WrapM(item) is { } wrap)
                    yield return wrap;
        }

        private static StdMod? WrapM(ProcessModule module)
        {
            try
            {
                _ = module.FileName;
            }
            catch (Exception)
            {
                return null;
            }
            var wrap = new StdMod(module);
            return wrap;
        }

        public Task<bool> Kill(IProcess proc)
        {
            var res = ProcExt.Kill(proc);
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