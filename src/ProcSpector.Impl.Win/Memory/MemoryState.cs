namespace ProcSpector.Impl.Win.Memory
{
    public enum MemoryState : uint
    {
        Commit = 0x1000,
        Reserve = 0x2000,
        Free = 0x10000
    }
}