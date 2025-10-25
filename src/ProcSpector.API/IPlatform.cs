namespace ProcSpector.API
{
    public interface IPlatform
    {
        ISystem1? System1 { get; }
        ISystem2? System2 { get; }
        ISystem3? System3 { get; }
    }
}