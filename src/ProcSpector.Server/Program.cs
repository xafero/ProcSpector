using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ProcSpector.Core;
using ProcSpector.Server.Config;
using ProcSpector.Server.Services;

namespace ProcSpector.Server
{
    public static class Program
    {
        private static AppSettings? _cfg;

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            _cfg = ConfigTool.ReadJsonObj<AppSettings>();
            if (_cfg.Server?.GetUrl() is { } url)
                builder.WebHost.UseUrls(url);

            builder.Services.AddGrpc();

            var app = builder.Build();

            app.MapGrpcService<InspectorService>();
            app.MapGet("/", () => "You need a gRPC client!");

            app.Run();
        }
    }
}