using ProcSpector.API;

namespace ProcSpector.Impl.Remote.Proxy
{
    public class RmModule : IModule
    {
        public long BaseAddress { get; set; }
        public string FileName { get; set; }
        public long Size { get; set; }
    }
}