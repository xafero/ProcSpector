namespace ProcSpector.Core.Plugins
{
    public interface IPlugin
    {
        string? Name { get; }
        string? Root { get; }
    }
}