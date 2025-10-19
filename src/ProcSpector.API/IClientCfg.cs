namespace ProcSpector.API
{
    public interface IClientCfg
    {
        string? Address { get; }

        int? Port { get; }
    }
}