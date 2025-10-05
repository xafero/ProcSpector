namespace ProcSpector.Comm
{
    public record HelloMsg : IMessage
    {
        public long Id => 0;

        public string? User { get; init; }
        public string? Host { get; init; }
    }
}