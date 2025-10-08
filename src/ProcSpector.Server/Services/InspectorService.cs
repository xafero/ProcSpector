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

        public override async Task<JsonRsp> GetHostName(JsonReq req, ServerCallContext ctx)
        {
            var a = await Sys.GetHostName();
            var b = new JsonRsp { Res = a.Wrap() };
            return b;
        }

        public override async Task<JsonRsp> GetUserName(JsonReq req, ServerCallContext ctx)
        {
            var a = await Sys.GetUserName();
            var b = new JsonRsp { Res = a.Wrap() };
            return b;
        }

        public override Task<JsonRsp> GetAllProcesses(JsonReq request, ServerCallContext context)
        {
            
            
            return base.GetAllProcesses(request, context);
        }

        public async Task<JsonRsp> GetAllProcesses2(JsonReq req, ServerCallContext ctx)
        {
            var a = await Sys.GetAllProcesses();
            var b = new JsonRsp { Res = a.Wrap() };
            return b;
        }

        public override async Task<JsonRsp> GetHandles(JsonReq req, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            var b = await Sys.GetHandles(a);
            var c = new JsonRsp { Res = b.Wrap() };
            return c;
        }

        public override async Task<JsonRsp> GetModules(JsonReq req, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            var b = await Sys.GetModules(a);
            var c = new JsonRsp { Res = b.Wrap() };
            return c;
        }

        public override async Task<JsonRsp> GetRegions(JsonReq req, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            var b = await Sys.GetRegions(a);
            var c = new JsonRsp { Res = b.Wrap() };
            return c;
        }

        public override async Task<JsonRsp> CreateMemSave(JsonReq req, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            var b = await Sys.CreateMemSave(a);
            var c = new JsonRsp { Res = b.Wrap() };
            return c;
        }

        public override async Task<JsonRsp> CreateMiniDump(JsonReq req, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            var b = await Sys.CreateMiniDump(a);
            var c = new JsonRsp { Res = b.Wrap() };
            return c;
        }

        public override async Task<JsonRsp> CreateScreenShot(JsonReq req, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            var b = await Sys.CreateScreenShot(a);
            var c = new JsonRsp { Res = b.Wrap() };
            return c;
        }

        public override async Task<JsonRsp> Kill(JsonReq req, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            var b = await Sys.Kill(a);
            var c = new JsonRsp { Res = b.Wrap() };
            return c;
        }
    }
}