using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using ProcSpector.API;
using ProcSpector.Core;
using ProcSpector.Grpc;
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
        public IClientCfg Cfg { get; }

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

        public ISystem System => this;

        public FeatureFlags Flags
        {
            get
            {
                var arg = new JsonReq();
                var req = Client.GetFlags(arg);
                var res = req.Res.Unwrap<FeatureFlags>();
                return res;
            }
        }

        public async Task<IUserInfo?> GetUserInfo()
        {
            var arg = new JsonReq();
            var req = await Client.GetUserInfoAsync(arg);
            var res = req.Res.Unwrap<IUserInfo>();
            return res;
        }

        public async IAsyncEnumerable<IProcess> GetProcesses()
        {
            var arg = new JsonReq();
            var req = Client.GetProcesses(arg);
            await foreach (var item in req.ResponseStream.ReadAllAsync())
            {
                var res = item.Res.Unwrap<IProcess>();
                if (res is not null)
                    yield return res;
            }
        }
    }
}