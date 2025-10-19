using System;
using Grpc.Net.Client;
using ProcSpector.API;
using ProcSpector.Comm;
using static ProcSpector.Grpc.Inspector;

namespace ProcSpector.Impl.Remote
{
    public sealed class RemotePlatform : IPlatform, ISystem, IDisposable
    {
        public RemotePlatform(IClientCfg cfg)
        {
            Cfg = cfg;
            (Channel, Client) = Connect(cfg);
        }

        private GrpcChannel Channel { get; }
        private InspectorClient Client { get; }
        private IClientCfg Cfg { get; }

        public void Dispose()
        {
            Channel.Dispose();
        }

        private static (GrpcChannel channel, InspectorClient client) Connect(IClientCfg cfg)
        {
            var opt = new GrpcChannelOptions { MaxRetryAttempts = 30 };
            var channel = GrpcChannel.ForAddress(cfg.GetUrl(), opt);
            var client = new InspectorClient(channel);
            return (channel, client);
        }
    }
}