using System;
using ByteSizeLib;
using ProcSpector.API;
using ProcSpector.API.Memory;

namespace ProcSpector.Impl.Remote.Proxy
{
    public class RmRegion : IMemRegion
    {
        public IntPtr BaseAddress { get; set; }
        public ByteSize Size { get; set; }
        public MemoryProtect Protection { get; set; }
        public MemoryState State { get; set; }
        public MemoryType Type { get; set; }

        public void CreateMemSave()
        {
            throw new NotImplementedException();
        }
    }
}