using System;
using ProcSpector.API;

#pragma warning disable CA1416

namespace ProcSpector.Impl.Win.Internal
{
    public static class Win32Ext
    {
        public static IFile? CreateMemSave(IProcess proc, WinPlatform winPlatform)
        {
            throw new NotImplementedException();
        }

        public static IFile? CreateMemSave(IMemRegion mem, WinPlatform winPlatform)
        {
            throw new NotImplementedException();
        }

        public static IFile? CreateScreenShot(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public static IFile? CreateScreenShot(IHandle handle)
        {
            throw new NotImplementedException();
        }

        public static IFile? CreateMiniDump(IProcess proc, WinPlatform winPlatform)
        {
            throw new NotImplementedException();
        }
    }
}