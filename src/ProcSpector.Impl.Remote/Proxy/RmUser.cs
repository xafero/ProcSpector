using ProcSpector.API;

namespace ProcSpector.Impl.Remote.Proxy
{
    public class RmUser : IUserInfo
    {
        public string? Host { get; set; }
        public string? Name { get; set; }
    }
}