using System;
using ByteSizeLib;
using ProcSpector.API;

namespace ProcSpector.Impl.Remote.Proxy
{
    public class RmModule : IModule
    {
        public IntPtr BaseAddress { get; set; }
        public string FileName { get; set; }
        public ByteSize Size { get; set; }

        public void OpenFolder()
        {
            throw new NotImplementedException();
        }
    }
}