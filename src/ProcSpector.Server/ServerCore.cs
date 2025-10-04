using System;
using System.IO;
using System.Net.Sockets;
using static ProcSpector.Core.StrTool;

namespace ProcSpector.Server
{
    internal static class ServerCore
    {
        internal static void StartLoop(object? sender)
        {
            var server = (TcpListener)sender!;
            Console.WriteLine($"Listening on {server.LocalEndpoint} ...");

            while (server.Server.IsBound)
            {
                try
                {
                    DoLoop(server);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($" [ERROR] {ex.Message}");
                }
            }
        }

        private static void DoLoop(TcpListener server)
        {
            using var client = server.AcceptTcpClient();
            Console.WriteLine($"Connection from {client.Client.RemoteEndPoint} !");

            using var stream = client.GetStream();
            using var reader = new StreamReader(stream, Enc);
            using var writer = new StreamWriter(stream, Enc);

            while (reader.ReadLine() is { } line)
            {
                Console.WriteLine($"Received: '{line}'");
                writer.WriteLine("OK");

                if (line.Equals("quit", Inv))
                {
                    client.Close();
                }
            }
        }
    }
}