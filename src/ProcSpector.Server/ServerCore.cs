using System;
using System.IO;
using System.Net.Sockets;
using ProcSpector.Comm;
using ProcSpector.Core;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using ProcSpector.API;
using ProcSpector.Impl;
using static ProcSpector.Core.StrTool;

// ReSharper disable AccessToDisposedClosure

namespace ProcSpector.Server
{
    internal static class ServerCore
    {
        private static BlockingCollection<IMessage> _responses = new();

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

            var writing = Task.Run(() =>
            {
                foreach (var message in _responses.GetConsumingEnumerable())
                    writer.WriteJson(message);
            });
            var reading = Task.Run(() =>
            {
                while (reader.ReadJson<RequestMsg>() is { } message)
                    RunThis(message, client);
            });
            Task.WaitAll(writing, reading);
        }

        private static void RunThis(RequestMsg req, TcpClient client)
        {
            var value = ExecThis(req, client);
            var type = value?.GetType() ?? typeof(object);
            var typeName = type.FullName;
            var res = new ResponseMsg
            {
                Id = req.Id, Type = typeName, Value = value
            };
            _responses.Add(res);
        }

        private static object? ExecThis(RequestMsg req, TcpClient client)
        {
            var plat = Factory.Platform.Value;
            var meth = req.Method.TrimOrNull() ?? "_";
            switch (meth)
            {
                case nameof(ISystem.UserName):
                    return plat.System.UserName;
                case nameof(ISystem.HostName):
                    return plat.System.HostName;
                case nameof(ISystem.GetAllProcesses):
                    return plat.System.GetAllProcesses();
                case nameof(ISystem.Quit):
                    client.Close();
                    return null;
                default:
                    throw new InvalidOperationException(meth);
            }
        }
    }
}