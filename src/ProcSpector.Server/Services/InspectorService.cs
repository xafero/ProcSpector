using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ProcSpector.API;
using ProcSpector.Comm;
using ProcSpector.Grpc;
using ProcSpector.Impl;
using ProcSpector.Impl.Remote.Proxy;

namespace ProcSpector.Server.Services
{
    public class InspectorService : Inspector.InspectorBase
    {
        private readonly ILogger<InspectorService> _log;

        public InspectorService(ILogger<InspectorService> log)
            => _log = log;

        private static ISystem Sys
            => Factory.Platform.Value.System;

        public override Task<JsonRsp> GetHostName(JsonReq req, ServerCallContext ctx)
            => Task.FromResult(new JsonRsp { Res = Sys.GetHostName().Wrap() });

        public override Task<JsonRsp> GetUserName(JsonReq req, ServerCallContext ctx)
            => Task.FromResult(new JsonRsp { Res = Sys.GetUserName().Wrap() });

        public override Task<JsonRsp> GetAllProcesses(JsonReq req, ServerCallContext ctx)
            => Task.FromResult(new JsonRsp { Res = Sys.GetAllProcesses().Wrap() });

        public override Task<JsonRsp> GetHandles(JsonReq req, ServerCallContext ctx)
            => Task.FromResult(new JsonRsp { Res = Sys.GetHandles(req.Arg.Unwrap<RmProcess>()!).Wrap() });

        public override Task<JsonRsp> GetModules(JsonReq req, ServerCallContext ctx)
            => Task.FromResult(new JsonRsp { Res = Sys.GetModules(req.Arg.Unwrap<RmProcess>()!).Wrap() });

        public override Task<JsonRsp> GetRegions(JsonReq req, ServerCallContext ctx)
            => Task.FromResult(new JsonRsp { Res = Sys.GetRegions(req.Arg.Unwrap<RmProcess>()!).Wrap() });

        public override Task<JsonRsp> CreateMemSave(JsonReq req, ServerCallContext ctx)
            => Task.FromResult(new JsonRsp { Res = Sys.CreateMemSave(req.Arg.Unwrap<RmProcess>()!).Wrap() });

        public override Task<JsonRsp> CreateMiniDump(JsonReq req, ServerCallContext ctx)
            => Task.FromResult(new JsonRsp { Res = Sys.CreateMiniDump(req.Arg.Unwrap<RmProcess>()!).Wrap() });

        public override Task<JsonRsp> CreateScreenShot(JsonReq req, ServerCallContext ctx)
            => Task.FromResult(new JsonRsp { Res = Sys.CreateScreenShot(req.Arg.Unwrap<RmProcess>()!).Wrap() });

        public override Task<JsonRsp> Kill(JsonReq req, ServerCallContext ctx)
            => Task.FromResult(new JsonRsp { Res = Sys.Kill(req.Arg.Unwrap<RmProcess>()!).Wrap() });
    }
}