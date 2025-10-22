using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcSpector.API
{
    public interface ISystem
    {
        FeatureFlags Flags { get; }

        Task<IUserInfo?> GetUserInfo();

        IAsyncEnumerable<IProcess> GetProcesses();

        IAsyncEnumerable<IModule> GetModules(IProcess proc);
        IAsyncEnumerable<IMemRegion> GetRegions(IProcess proc);
        IAsyncEnumerable<IHandle> GetHandles(IProcess proc);

        Task<IFile?> CreateScreenShot(IProcess proc);
        Task<IFile?> CreateScreenShot(IHandle handle);

        Task<IFile?> CreateMemSave(IProcess proc);
        Task<IFile?> CreateMemSave(IMemRegion mem);

        Task<IFile?> CreateMiniDump(IProcess proc);

        Task<bool> Kill(IProcess proc);

        Task<bool> OpenFolder(IProcess proc);
        Task<bool> OpenFolder(IModule mod);
    }
}