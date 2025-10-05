using System;

namespace ProcSpector.Impl.Win.Internal
{
    public record WinStruct(
        IntPtr WindowHandle,
        uint ProcessId,
        uint ThreadId,
        IntPtr? ParentHandle
    );
}