using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ProcSpector.Core;
using ProcSpector.Server.Config;
using static ProcSpector.Server.ServerCore;

namespace ProcSpector.Server
{
    internal static class Program
    {
        private static void Main()
        {
            var settings = ConfigTool.ReadJsonObj<AppSettings>();

            var addr = NetTool.Parse(settings.Host?.Address) ?? IPAddress.Any;
            var port = settings.Host?.Port ?? 8093;
            var backlog = settings.Host?.Backlog ?? 10;

            using var server = new TcpListener(addr, port);
            server.Start(backlog);

            var thread = new Thread(StartLoop) { IsBackground = true, Name = "Loop" };
            thread.Start(server);

            Console.ReadLine();
            thread.Interrupt();
        }
    }
}