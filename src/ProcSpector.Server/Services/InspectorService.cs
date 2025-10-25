using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using ProcSpector.API;
using ProcSpector.Core;
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

        private static ISystem1 Sys1 => Factory.Platform.Value.System1!;
        private static ISystem2 Sys2 => Factory.Platform.Value.System2!;

        public override async Task<JsonRsp> GetUserInfo(JsonReq req, ServerCallContext context)
        {
            var a = await Sys1.GetUserInfo();
            var b = new JsonRsp { Res = a.Wrap() };
            return b;
        }

        public override async Task GetProcesses(JsonReq req, IServerStreamWriter<JsonRsp> rsp, ServerCallContext ctx)
        {
            await foreach (var a in Sys1.GetProcesses())
                await rsp.WriteAsync(new JsonRsp { Res = a.Wrap() });
        }

        public override async Task GetModules(JsonReq req, IServerStreamWriter<JsonRsp> rsp, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            await foreach (var b in Sys1.GetModules(a))
                await rsp.WriteAsync(new JsonRsp { Res = b.Wrap() });
        }

        public override async Task GetHandles(JsonReq req, IServerStreamWriter<JsonRsp> rsp, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            await foreach (var b in Sys2.GetHandles(a))
                await rsp.WriteAsync(new JsonRsp { Res = b.Wrap() });
        }

        public override async Task GetRegions(JsonReq req, IServerStreamWriter<JsonRsp> rsp, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            await foreach (var b in Sys2.GetRegions(a))
                await rsp.WriteAsync(new JsonRsp { Res = b.Wrap() });
        }

        public override async Task<JsonRsp> CreateMemSaveP(JsonReq req, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            var b = await Sys2.CreateMemSave(a);
            var c = new JsonRsp { Res = b.Wrap() };
            return c;
        }

        public override async Task<JsonRsp> CreateMemSaveR(JsonReq req, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmRegion>()!;
            var b = await Sys2.CreateMemSave(a);
            var c = new JsonRsp { Res = b.Wrap() };
            return c;
        }

        public override async Task<JsonRsp> CreateMiniDump(JsonReq req, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            var b = await Sys2.CreateMiniDump(a);
            var c = new JsonRsp { Res = b.Wrap() };
            return c;
        }

        public override async Task<JsonRsp> CreateScreenShotP(JsonReq req, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            var b = await Sys2.CreateScreenShot(a);
            var c = new JsonRsp { Res = b.Wrap() };
            return c;
        }

        public override async Task<JsonRsp> CreateScreenShotH(JsonReq req, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmHandle>()!;
            var b = await Sys2.CreateScreenShot(a);
            var c = new JsonRsp { Res = b.Wrap() };
            return c;
        }

        public override async Task<JsonRsp> Kill(JsonReq req, ServerCallContext ctx)
        {
            var a = req.Arg.Unwrap<RmProcess>()!;
            var b = await Sys1.Kill(a);
            var c = new JsonRsp { Res = b.Wrap() };
            return c;
        }
    }
}