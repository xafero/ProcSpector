using System;
using System.Runtime.InteropServices;
using ProcSpector.API;
using ProcSpector.Impl.Net;
using ProcSpector.Impl.Remote2;
using ProcSpector.Impl.Win;

namespace ProcSpector.Impl
{
    public static class Factory
    {
        public static IClientCfg? ClientCfg { private get; set; }

        private static IPlatform GetPlatform()
        {
            if (ClientCfg is { } cfg)
            {
                return new RemotePlatform(cfg);
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