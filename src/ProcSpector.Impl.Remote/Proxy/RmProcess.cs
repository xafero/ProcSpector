using System;
using ByteSizeLib;
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
        public ByteSize WorkingSet { get; set; }
        public ByteSize PagedMem { get; set; }
        public string? FileName { get; set; }

        public void Kill()
        {
            throw new NotImplementedException();
        }

        public void CreateMemSave()
        {
            throw new NotImplementedException();
        }

        public void CreateScreenShot()
        {
            throw new NotImplementedException();
        }

        public void CreateMiniDump()
        {
            throw new NotImplementedException();
        }

        public void OpenFolder()
        {
            throw new NotImplementedException();
        }
    }
}