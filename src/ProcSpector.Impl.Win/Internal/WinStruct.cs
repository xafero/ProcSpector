using System;

namespace ProcSpector.Lib
{
    public record WinStruct(
        IntPtr WindowHandle,
        uint ProcessId,
        uint ThreadId,
        IntPtr? ParentHandle
    );
}