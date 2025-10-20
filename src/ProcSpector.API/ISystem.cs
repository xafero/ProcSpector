using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcSpector.API
{
    public interface ISystem
    {
        FeatureFlags Flags { get; }

        Task<IUserInfo?> GetUserInfo();

        IAsyncEnumerable<IProcess> GetProcesses();
    }
}