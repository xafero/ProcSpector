// ReSharper disable ClassNeverInstantiated.Global

using ProcSpector.API;

namespace ProcSpector.Config
{
    public class ClientSettings : IClientCfg
    {
        public string? Address { get; set; }
        public int? Port { get; set; }
    }
}