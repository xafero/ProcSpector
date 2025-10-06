using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcSpector.API
{
    public interface ISystem
    {
        IAsyncEnumerable<IProcess> GetAllProcesses();
        IAsyncEnumerable<IModule> GetModules(IProcess proc);
        IAsyncEnumerable<IMemRegion> GetRegions(IProcess proc);
        IAsyncEnumerable<IHandle> GetHandles(IProcess proc);

        Task<string> GetHostName();
        Task<string> GetUserName();

        Task<bool> CreateScreenShot(IProcess proc);
        Task<bool> CreateScreenShot(IHandle handle);

        Task<bool> CreateMemSave(IProcess proc);
        Task<bool> CreateMemSave(IMemRegion mem);

        Task<bool> CreateMiniDump(IProcess proc);

        Task<bool> Kill(IProcess proc);

        Task<bool> OpenFolder(IProcess proc);
        Task<bool> OpenFolder(IModule mod);

        Task<bool> Quit();
    }
}