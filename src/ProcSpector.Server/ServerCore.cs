using System;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using ProcSpector.API;
using ProcSpector.Comm;
using ProcSpector.Core;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static ProcSpector.Core.StrTool;

// ReSharper disable AccessToDisposedClosure

namespace ProcSpector.Server
{
    internal static class ServerCore
    {
        private static BlockingCollection<IMessage> _requests = new();
        private static Dictionary<long, IMessage> _responses = new();

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

            var hm = reader.ReadJson<HelloMsg>()!;
            Console.WriteLine($"User '{hm.User}' on host '{hm.Host}' connected.");

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