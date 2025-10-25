using System.Threading.Tasks;

namespace ProcSpector.API
{
    public interface ISystem3
    {
        Task<bool> OpenFolder(IProcess proc);
        Task<bool> OpenFolder(IModule mod);
    }
}