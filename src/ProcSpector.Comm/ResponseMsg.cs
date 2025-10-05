namespace ProcSpector.Comm
{
    public record ResponseMsg : IMessage
    {
        public long Id { get; set; }
    }
}