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

        Task<IFile?> CreateScreenShot(IHandle handle);

        Task<IFile?> CreateMemSave(IMemRegion mem);
    }
}