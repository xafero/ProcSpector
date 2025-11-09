using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcSpector.API
{
    public interface ISystem2
    {
        IAsyncEnumerable<IHandle> GetHandles(IProcess proc);
        IAsyncEnumerable<IMemRegion> GetRegions(IProcess proc);

        Task<IFile?> CreateScreenShot(IProcess proc);
        Task<IFile?> CreateScreenShot(IHandle handle);

        Task<IFile?> CreateMemSave(IProcess proc);
        Task<IFile?> CreateMemSave(IMemRegion mem);

        Task<IFile?> CreateMiniDump(IProcess proc);

        Task<bool> Activate(IProcess proc);
        Task<bool> Activate(IHandle handle);

        Task<bool> SetMouse(IHandle handle, int x, int y);
        Task<bool> SendKey(IHandle handle, string mode, string key);
        Task<bool> SendModKey(IHandle handle, string mod, string key);
        Task<bool> Sleep(string mode, double val);
    }
}