using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            var channel = GrpcChannel.ForAddress($"http://{cfg.Address}:{cfg.Port}");
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

        public async Task<IEnumerable<IProcess>> GetAllProcesses()
        {
            var a = new JsonReq();
            var b = await Client.GetAllProcessesAsync(a);
            var c = b.Res.Unwrap<RmProcess[]>();
            return c ?? [];
        }

        public async Task<IEnumerable<IModule>> GetModules(IProcess proc)
        {
            var a = new JsonReq { Arg = proc.Wrap() };
            var b = await Client.GetModulesAsync(a);
            var c = b.Res.Unwrap<RmModule[]>();
            return c ?? [];
        }

        public async Task<IEnumerable<IMemRegion>> GetRegions(IProcess proc)
        {
            var a = new JsonReq { Arg = proc.Wrap() };
            var b = await Client.GetRegionsAsync(a);
            var c = b.Res.Unwrap<RmRegion[]>();
            return c ?? [];
        }

        public async Task<IEnumerable<IHandle>> GetHandles(IProcess proc)
        {
            var a = new JsonReq { Arg = proc.Wrap() };
            var b = await Client.GetHandlesAsync(a);
            var c = b.Res.Unwrap<RmHandle[]>();
            return c ?? [];
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