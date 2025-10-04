namespace ProcSpector.Comm
{
    public record HelloMsg : IMessage
    {
        public string? User { get; init; }
        public string? Host { get; init; }
    }
}