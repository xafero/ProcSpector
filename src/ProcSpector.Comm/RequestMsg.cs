using System.Threading;

namespace ProcSpector.Comm
{
    public record RequestMsg : IMessage
    {
        public long Id { get; set; } = Interlocked.Increment(ref _idCnt);
        public string? Method { get; set; }

        private static long _idCnt;
    }
}