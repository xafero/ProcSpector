namespace ProcSpector.Lib.Memory
{
    public enum MemoryType : uint
    {
        Private = 0x20000,
        Mapped = 0x40000,
        Image = 0x1000000
    }
}