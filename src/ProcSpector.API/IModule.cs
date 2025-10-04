using System;
using ByteSizeLib;

namespace ProcSpector.API
{
    public interface IModule
    {
        IntPtr BaseAddress { get; }
        string FileName { get; }
        ByteSize Size { get; }

        void OpenFolder();
    }
}