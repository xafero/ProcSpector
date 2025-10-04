using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using ProcSpector.API;
using ProcSpector.Comm;
using ProcSpector.Core;
using static ProcSpector.Core.StrTool;

namespace ProcSpector.Impl.Remote
{
    internal static class ClientCore
    {
        internal static void StartLoop(object? sender)
        {
            var platform = (RemotePlatform)sender!;
            while (platform.Cfg is { } cfg)
            {
                try
                {
                    DoLoop(cfg);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($" [ERROR] {ex.Message}");
                }
            }
        }

        private static void DoLoop(IClientCfg cfg)
        {
            var hostName = cfg.Address ?? "localhost";
            var port = cfg.Port ?? 8093;

            using var client = new TcpClient(hostName, port);
            Debug.WriteLine($"Connection to {client.Client.RemoteEndPoint} !");

            using var stream = client.GetStream();
            using var reader = new StreamReader(stream, Enc);
            using var writer = new StreamWriter(stream, Enc);

            writer.WriteJson(new HelloMsg { User = Environment.UserName, Host = Environment.MachineName });

            while (reader.ReadLine() is { } line)
            {
                Debug.WriteLine($"Received: '{line}'");
            }
        }
    }
}