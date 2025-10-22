using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcSpector.API
{
    public interface ISystem
    {
        FeatureFlags Flags { get; }

        Task<IUserInfo?> GetUserInfo();

        IAsyncEnumerable<IProcess> GetProcesses();

        IAsyncEnumerable<IHandle> GetHandles(IProcess proc);

        IAsyncEnumerable<IMemRegion> GetRegions(IProcess proc);

        IAsyncEnumerable<IModule> GetModules(IProcess proc);

        Task<IFile?> CreateScreenShot(IProcess proc);

        Task<IFile?> CreateScreenShot(IHandle handle);

        Task<IFile?> CreateMemSave(IProcess proc);

        Task<IFile?> CreateMemSave(IMemRegion mem);

        Task<IFile?> CreateMiniDump(IProcess proc);

        Task OpenFolder(IProcess proc);

        Task OpenFolder(IModule mod);

        Task Kill(IProcess proc);
    }
}