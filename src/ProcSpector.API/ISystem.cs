using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcSpector.API
{
    public interface ISystem3
    {
        Task<bool> OpenFolder(IProcess proc);
        Task<bool> OpenFolder(IModule mod);
    }

    public interface ISystem2
    {
        IAsyncEnumerable<IHandle> GetHandles(IProcess proc);
        IAsyncEnumerable<IMemRegion> GetRegions(IProcess proc);

        Task<IFile?> CreateScreenShot(IProcess proc);
        Task<IFile?> CreateScreenShot(IHandle handle);

        Task<IFile?> CreateMemSave(IProcess proc);
        Task<IFile?> CreateMemSave(IMemRegion mem);

        Task<IFile?> CreateMiniDump(IProcess proc);
    }

    public interface ISystem1
    {
        FeatureFlags Flags { get; }

        Task<IUserInfo?> GetUserInfo();

        IAsyncEnumerable<IProcess> GetProcesses();
        IAsyncEnumerable<IModule> GetModules(IProcess proc);

        Task<bool> Kill(IProcess proc);
    }
}