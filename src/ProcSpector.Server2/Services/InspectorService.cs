using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ProcSpector.API;
using ProcSpector.Grpc;
using ProcSpector.Impl;

namespace ProcSpector.Server2.Services
{
    public class InspectorService : Inspector.InspectorBase
    {
        private readonly ILogger<InspectorService> _log;

        public InspectorService(ILogger<InspectorService> log)
            => _log = log;

        private static ISystem Sys
            => Factory.Platform.Value.System;

        public override Task<StrReply> GetHostName(StrRequest request, ServerCallContext context)
            => Task.FromResult(new StrReply { Result = Sys.HostName });

        public override Task<StrReply> GetUserName(StrRequest request, ServerCallContext context)
            => Task.FromResult(new StrReply { Result = Sys.UserName });
    }
}