using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ProcSpector.API;
using ProcSpector.Comm;
using ProcSpector.Grpc;
using ProcSpector.Impl;
using ProcSpector.Impl.Remote.Proxy;

// ReSharper disable NotAccessedField.Local

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

        public override async Task GetAllProcesses(JsonReq req, IServerStreamWriter<JsonRsp> rsp, ServerCallContext ctx)
        {
            await foreach (var a in Sys.GetAllProcesses())
                await rsp.WriteAsync(new JsonRsp { Res = a.Wrap() });
        }

        public override async Task GetHandles(JsonReq req, IServerStreamWriter<JsonRsp> rsp, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            await foreach (var b in Sys.GetHandles(a))
                await rsp.WriteAsync(new JsonRsp { Res = b.Wrap() });
        }

        public override async Task GetModules(JsonReq req, IServerStreamWriter<JsonRsp> rsp, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            await foreach (var b in Sys.GetModules(a))
                await rsp.WriteAsync(new JsonRsp { Res = b.Wrap() });
        }

        public override async Task GetRegions(JsonReq req, IServerStreamWriter<JsonRsp> rsp, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            await foreach (var b in Sys.GetRegions(a))
                await rsp.WriteAsync(new JsonRsp { Res = b.Wrap() });
        }

        public override async Task<JsonRsp> CreateMemSaveP(JsonReq req, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            var b = await Sys.CreateMemSave(a);
            var c = new JsonRsp { Res = b.Wrap() };
            return c;
        }

        public override async Task<JsonRsp> CreateMemSaveR(JsonReq req, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmRegion>()!;
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

        public override async Task<JsonRsp> CreateScreenShotP(JsonReq req, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            var b = await Sys.CreateScreenShot(a);
            var c = new JsonRsp { Res = b.Wrap() };
            return c;
        }

        public override async Task<JsonRsp> CreateScreenShotH(JsonReq req, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmHandle>()!;
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