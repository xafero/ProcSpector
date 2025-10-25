using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcSpector.API
{
    public interface ISystem1
    {
        FeatureFlags Flags { get; }

        Task<IUserInfo?> GetUserInfo();

        IAsyncEnumerable<IProcess> GetProcesses();
        IAsyncEnumerable<IModule> GetModules(IProcess proc);

        Task<bool> Kill(IProcess proc);
    }
}