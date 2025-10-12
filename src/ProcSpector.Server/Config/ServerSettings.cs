// ReSharper disable ClassNeverInstantiated.Global

using ProcSpector.API;

namespace ProcSpector.Server.Config
{
    public class ServerSettings : IClientCfg
    {
        public string? Address { get; set; }
        public int? Port { get; set; }
    }
}