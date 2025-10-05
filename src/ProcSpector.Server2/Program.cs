using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ProcSpector.Server2.Services;

namespace ProcSpector.Server2
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddGrpc();

            var app = builder.Build();

            app.MapGrpcService<GreeterService>();
            app.MapGet("/", () => "You need a gRPC client!");

            app.Run();
        }
    }
}