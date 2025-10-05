using System;
using System.Collections.Generic;
using Grpc.Net.Client;
using ProcSpector.API;
using ProcSpector.Grpc;

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

        public ISystem System => this;

        public string HostName
        {
            get
            {
                var reply = _client.GetHostName(
                    new StrRequest { Method = nameof(HostName) }
                );
                return reply.Result;
            }
        }

        public string UserName
        {
            get
            {
                var reply = _client.GetUserName(
                    new StrRequest { Method = nameof(UserName) }
                );
                return reply.Result;
            }
        }

        public IEnumerable<IProcess> GetAllProcesses()
        {
            // TODO
            return [];
        }

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

        public void Kill(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public void CreateMemSave(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public void CreateScreenShot(IProcess proc)
        {
            throw new NotImplementedException();
        }

        public void CreateMiniDump(IProcess proc)
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