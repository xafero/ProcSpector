using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProcSpector.API;

namespace ProcSpector.Impl.Net
{
    public class NetPlatform : IPlatform, ISystem
    {
        public ISystem System => this;

        public IAsyncEnumerable<IProcess> GetAllProcesses()
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<IModule> GetModules(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<IMemRegion> GetRegions(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public IAsyncEnumerable<IHandle> GetHandles(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetHostName()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserName()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateScreenShot(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateScreenShot(IHandle handle)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateMemSave(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateMemSave(IMemRegion mem)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CreateMiniDump(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Kill(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public Task<bool> OpenFolder(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public Task<bool> OpenFolder(IModule mod)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Quit()
        {
            throw new NotImplementedException();
        }
    }
}