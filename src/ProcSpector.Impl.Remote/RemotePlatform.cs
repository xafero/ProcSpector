using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using ProcSpector.API;
using ProcSpector.Comm;
using ProcSpector.Grpc;
using ProcSpector.Impl.Remote.Proxy;
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

        public async Task<string> GetHostName()
        {
            var a = new JsonReq();
            var b = await Client.GetHostNameAsync(a);
            var c = b.Res.Unwrap<string>();
            return c ?? "?";
        }

        public async Task<string> GetUserName()
        {
            var a = new JsonReq();
            var b = await Client.GetUserNameAsync(a);
            var c = b.Res.Unwrap<string>();
            return c ?? "?";
        }

        public async IAsyncEnumerable<IProcess> GetAllProcesses()
        {
            var a = new JsonReq();
            await foreach (var b in Client.GetAllProcesses(a).ResponseStream.ReadAllAsync())
                if (b.Res.Unwrap<RmProcess>() is { } c)
                    yield return c;
        }

        public async IAsyncEnumerable<IModule> GetModules(IProcess proc)
        {
            var a = new JsonReq { Arg = proc.Wrap() };
            await foreach (var b in Client.GetModules(a).ResponseStream.ReadAllAsync())
                if (b.Res.Unwrap<RmModule>() is { } c)
                    yield return c;
        }

        public async IAsyncEnumerable<IMemRegion> GetRegions(IProcess proc)
        {
            var a = new JsonReq { Arg = proc.Wrap() };
            await foreach (var b in Client.GetRegions(a).ResponseStream.ReadAllAsync())
                if (b.Res.Unwrap<RmRegion>() is { } c)
                    yield return c;
        }

        public async IAsyncEnumerable<IHandle> GetHandles(IProcess proc)
        {
            var a = new JsonReq { Arg = proc.Wrap() };
            await foreach (var b in Client.GetHandles(a).ResponseStream.ReadAllAsync())
                if (b.Res.Unwrap<RmHandle>() is { } c)
                    yield return c;
        }

        public async Task<bool> Kill(IProcess proc)
        {
            var a = new JsonReq { Arg = proc.Wrap() };
            var b = await Client.KillAsync(a);
            var c = b.Res.Unwrap<bool>();
            return c;
        }

        public async Task<bool> CreateScreenShot(IProcess proc)
        {
            var a = new JsonReq { Arg = proc.Wrap() };
            var b = await Client.CreateScreenShotAsync(a);
            var c = b.Res.Unwrap<bool>();
            return c;
        }

        public async Task<bool> CreateScreenShot(IHandle handle)
        {
            var a = new JsonReq { Arg = handle.Wrap() };
            var b = await Client.CreateScreenShotAsync(a);
            var c = b.Res.Unwrap<bool>();
            return c;
        }

        public async Task<bool> CreateMemSave(IProcess proc)
        {
            var a = new JsonReq { Arg = proc.Wrap() };
            var b = await Client.CreateMemSaveAsync(a);
            var c = b.Res.Unwrap<bool>();
            return c;
        }

        public async Task<bool> CreateMemSave(IMemRegion mem)
        {
            var a = new JsonReq { Arg = mem.Wrap() };
            var b = await Client.CreateMemSaveAsync(a);
            var c = b.Res.Unwrap<bool>();
            return c;
        }

        public async Task<bool> CreateMiniDump(IProcess proc)
        {
            var a = new JsonReq { Arg = proc.Wrap() };
            var b = await Client.CreateMiniDumpAsync(a);
            var c = b.Res.Unwrap<bool>();
            return c;
        }

        public Task<bool> OpenFolder(IProcess proc)
        {
            // TODO
            return Task.FromResult(false);
        }

        public Task<bool> OpenFolder(IModule mod)
        {
            // TODO
            return Task.FromResult(false);
        }

        public Task<bool> Quit()
        {
            // TODO
            return Task.FromResult(false);
        }
    }
}