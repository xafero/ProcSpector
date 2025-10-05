using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ProcSpector.API;
using ProcSpector.Grpc;
using ProcSpector.Impl;

namespace ProcSpector.Server.Services
{
    public class InspectorService : Inspector.InspectorBase
    {
        private readonly ILogger<InspectorService> _log;

        public InspectorService(ILogger<InspectorService> log)
            => _log = log;

        private static ISystem Sys
            => Factory.Platform.Value.System;

        public override Task<JsonRsp> GetHostName(JsonReq req, ServerCallContext context)
            => Task.FromResult(new JsonRsp { Res = Sys.HostName });

        public override Task<JsonRsp> GetUserName(JsonReq req, ServerCallContext context)
            => Task.FromResult(new JsonRsp { Res = Sys.UserName });
    }
}