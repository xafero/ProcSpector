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

namespace ProcSpector.Impl.Remote
{
    internal static class ClientCore
    {
        private static BlockingCollection<IMessage> _requests = new();
        private static Dictionary<long, IMessage> _responses = new();

        internal static void StartLoop(object? sender)
        {
            var platform = (RemotePlatform)sender!;
            Debug.WriteLine("Connecting ...");

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

            var writing = Task.Run(() =>
            {
                foreach (var message in _requests.GetConsumingEnumerable())
                    writer.WriteJson(message);
            });
            var reading = Task.Run(() =>
            {
                while (reader.ReadJson<ResponseMsg>() is { } message)
                    _responses[message.Id] = message;
            });
            Task.WaitAll(writing, reading);
        }

        public static IMessage WaitFor(IMessage item, int delay = 100)
        {
            _requests.Add(item);

            var id = item.Id;
            IMessage? response;
            while (!_responses.TryGetValue(id, out response))
                Thread.Sleep(delay);
            _responses.Remove(id);
            return response;
        }
    }
}