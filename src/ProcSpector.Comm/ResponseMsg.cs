namespace ProcSpector.Comm
{
    public record ResponseMsg : IMessage
    {
        public long Id { get; set; }

        public string? Type { get; set; }

        public object? Value { get; set; }
    }
}