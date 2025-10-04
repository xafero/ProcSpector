namespace ProcSpector.Comm
{
    public record RequestMsg : IMessage
    {
        public string? Method { get; set; }
    }
}