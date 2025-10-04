using System;
using System.Runtime.InteropServices;
using ProcSpector.API;
using ProcSpector.Impl.Net;
using ProcSpector.Impl.Remote;
using ProcSpector.Impl.Win;

namespace ProcSpector.Impl
{
    public static class Factory
    {
        public static bool useRemote = false;

        private static IPlatform GetPlatform()
        {
            if (useRemote)
            {
                return new RemotePlatform();
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new WinPlatform();
            }
            if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ||
                RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return new NetPlatform();
            }
            throw new InvalidOperationException(RuntimeInformation.OSDescription);
        }

        public static Lazy<IPlatform> Platform { get; } = new(GetPlatform);
    }
}