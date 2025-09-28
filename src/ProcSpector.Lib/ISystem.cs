namespace ProcSpector.Lib
{
    public interface ISystem
    {
        IProcess[] Processes { get; }
    }
}