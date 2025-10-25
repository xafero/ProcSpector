using ProcSpector.API;

namespace ProcSpector.Impl.Remote.Proxy
{
    public class RmRegion : IMemRegion
    {
        public long BaseAddress { get; set; }
        public long Size { get; set; }
        public string? Protection { get; set; }
        public string? State { get; set; }
        public string? Type { get; set; }
    }
}