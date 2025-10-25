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
    public sealed class RemotePlatform : IPlatform, ISystem1, ISystem2, IDisposable
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

        public ISystem1 System1 => this;
        public ISystem2 System2 => this;
        public ISystem3? System3 => null;

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

        public async IAsyncEnumerable<IHandle> GetHandles(IProcess proc)
        {
            var arg = new JsonReq();
            var req = Client.GetHandles(arg);
            await foreach (var item in req.ResponseStream.ReadAllAsync())
            {
                var res = item.Res.Unwrap<IHandle>();
                if (res is not null)
                    yield return res;
            }
        }

        public async IAsyncEnumerable<IMemRegion> GetRegions(IProcess proc)
        {
            var arg = new JsonReq();
            var req = Client.GetRegions(arg);
            await foreach (var item in req.ResponseStream.ReadAllAsync())
            {
                var res = item.Res.Unwrap<IMemRegion>();
                if (res is not null)
                    yield return res;
            }
        }

        public async IAsyncEnumerable<IModule> GetModules(IProcess proc)
        {
            var arg = new JsonReq();
            var req = Client.GetModules(arg);
            await foreach (var item in req.ResponseStream.ReadAllAsync())
            {
                var res = item.Res.Unwrap<IModule>();
                if (res is not null)
                    yield return res;
            }
        }

        public async Task<IFile?> CreateScreenShot(IHandle handle)
        {
            var arg = new JsonReq();
            var req = await Client.CreateScreenShotHAsync(arg);
            var res = req.Res.Unwrap<IFile>();
            return res;
        }

        public async Task<IFile?> CreateScreenShot(IProcess proc)
        {
            var arg = new JsonReq();
            var req = await Client.CreateScreenShotPAsync(arg);
            var res = req.Res.Unwrap<IFile>();
            return res;
        }

        public async Task<IFile?> CreateMemSave(IProcess proc)
        {
            var arg = new JsonReq();
            var req = await Client.CreateMemSavePAsync(arg);
            var res = req.Res.Unwrap<IFile>();
            return res;
        }

        public async Task<IFile?> CreateMemSave(IMemRegion mem)
        {
            var arg = new JsonReq();
            var req = await Client.CreateMemSaveRAsync(arg);
            var res = req.Res.Unwrap<IFile>();
            return res;
        }

        public async Task<IFile?> CreateMiniDump(IProcess proc)
        {
            var arg = new JsonReq();
            var req = await Client.CreateMiniDumpAsync(arg);
            var res = req.Res.Unwrap<IFile>();
            return res;
        }

        public async Task<bool> Kill(IProcess proc)
        {
            var arg = new JsonReq();
            var req = await Client.KillAsync(arg);
            var res = req.Res.Unwrap<bool>();
            return res;
        }
    }
}