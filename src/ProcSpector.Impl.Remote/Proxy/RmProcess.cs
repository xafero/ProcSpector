using System;
using ProcSpector.API;

namespace ProcSpector.Impl.Remote.Proxy
{
    public class RmProcess : IProcess
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartTime { get; set; }
        public int Threads { get; set; }
        public int Handles { get; set; }
        public long WorkingSet { get; set; }
        public long PagedMem { get; set; }
        public string? FileName { get; set; }
    }
}