using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProcSpector.Server.Services;

namespace ProcSpector.Server
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGrpc();

            var app = builder.Build();

            app.MapGrpcService<InspectorService>();
            app.MapGet("/", () => "You need a gRPC client!");

            app.Run();
        }
    }
}