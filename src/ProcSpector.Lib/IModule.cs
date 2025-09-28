using System;
using ByteSizeLib;

namespace ProcSpector.Lib
{
    public interface IModule
    {
        IntPtr BaseAddress { get; }
        string FileName { get; }
        ByteSize Size { get; }
    }
}