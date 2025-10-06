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
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMemRegion> GetRegions(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IHandle> GetHandles(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public void OpenFolder(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public void OpenFolder(IModule mod)
        {
            throw new NotImplementedException();
        }

        public bool Kill(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public bool CreateMemSave(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public bool CreateScreenShot(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public bool CreateMiniDump(IProcess proc)
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