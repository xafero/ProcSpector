using ProcSpector.API;

namespace ProcSpector.Impl.Remote.Proxy
{
    public class RmHandle : IHandle
    {
        public long? Parent { get; set; }
        public long? Handle { get; set; }
        public string? Title { get; set; }
        public int? X { get; set; }
        public int? Y { get; set; }
        public int? W { get; set; }
        public int? H { get; set; }
        public string? Class { get; set; }
    }
}