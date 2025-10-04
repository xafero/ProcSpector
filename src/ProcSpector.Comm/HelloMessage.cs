namespace ProcSpector.Comm
{
    public record HelloMessage : IMessage
    {
        public string? User { get; init; }
        public string? Host { get; init; }
    }
}