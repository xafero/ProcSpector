using System;
using System.Collections.Generic;
using Grpc.Net.Client;
using ProcSpector.API;
using ProcSpector.Comm;
using ProcSpector.Grpc;
using ProcSpector.Impl.Remote.Proxy;

namespace ProcSpector.Impl.Remote
{
    public sealed class RemotePlatform : IPlatform, ISystem, IDisposable
    {
        internal IClientCfg Cfg { get; }

        private readonly GrpcChannel _channel;
        private readonly Inspector.InspectorClient _client;

        public RemotePlatform(IClientCfg cfg)
        {
            Cfg = cfg;
            _channel = GrpcChannel.ForAddress($"http://{cfg.Address}:{cfg.Port}");
            _client = new Inspector.InspectorClient(_channel);
        }

        public ISystem System
            => this;

        public string HostName
            => _client.GetHostName(new JsonReq()).Res.Unwrap<string>() ?? "";

        public string UserName
            => _client.GetUserName(new JsonReq()).Res.Unwrap<string>() ?? "";

        public IEnumerable<IProcess> GetAllProcesses()
            => _client.GetAllProcesses(new JsonReq()).Res.Unwrap<RmProcess[]>() ?? [];

        public IEnumerable<IModule> GetModules(IProcess proc)
            => _client.GetModules(new JsonReq()).Res.Unwrap<RmModule[]>() ?? [];

        public IEnumerable<IMemRegion> GetRegions(IProcess proc)
            => _client.GetRegions(new JsonReq()).Res.Unwrap<RmRegion[]>() ?? [];

        public IEnumerable<IHandle> GetHandles(IProcess proc)
            => _client.GetHandles(new JsonReq()).Res.Unwrap<RmHandle[]>() ?? [];

        public bool Kill(IProcess proc)
            => _client.Kill(new JsonReq()).Res.Unwrap<bool>();

        public bool CreateMemSave(IProcess proc)
            => _client.CreateMemSave(new JsonReq()).Res.Unwrap<bool>();

        public bool CreateScreenShot(IProcess proc)
            => _client.CreateScreenShot(new JsonReq()).Res.Unwrap<bool>();

        public bool CreateMiniDump(IProcess proc)
            => _client.CreateMiniDump(new JsonReq()).Res.Unwrap<bool>();

        public void OpenFolder(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public void OpenFolder(IModule mod)
        {
            throw new NotImplementedException();
        }

        public void Quit()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _channel.Dispose();
        }
    }
}