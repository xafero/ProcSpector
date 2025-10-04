using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ProcSpector.Server
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var addr = IPAddress.Any;
            var port = 8093;
            var backlog = 10;

            using var server = new TcpListener(addr, port);
            server.Start(backlog);

            var thread = new Thread(StartLoop) { IsBackground = true, Name = "Loop" };
            thread.Start(server);

            Console.ReadLine();
            thread.Interrupt();
        }

        private static void StartLoop(object? sender)
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

            var enc = Encoding.UTF8;
            var inv = StringComparison.InvariantCultureIgnoreCase;

            using var stream = client.GetStream();
            using var reader = new StreamReader(stream, enc);
            using var writer = new StreamWriter(stream, enc);

            while (reader.ReadLine() is { } line)
            {
                Console.WriteLine($"Received: '{line}'");
                writer.WriteLine("OK");

                if (line.Equals("quit", inv))
                {
                    client.Close();
                }
            }
        }
    }
}