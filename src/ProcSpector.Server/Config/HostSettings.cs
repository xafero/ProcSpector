// ReSharper disable ClassNeverInstantiated.Global

namespace ProcSpector.Server.Config
{
    public class HostSettings
    {
        public string? Address { get; set; }
        public int Port { get; set; }
        public int Backlog { get; set; }
    }
}