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

        Task<IFile?> CreateScreenShot(IHandle handle);

        Task<IFile?> CreateMemSave(IMemRegion mem);

        Task OpenFolder(IModule mod);
    }
}