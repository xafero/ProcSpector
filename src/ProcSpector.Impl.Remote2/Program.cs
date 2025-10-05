using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using GrpcGreeter;

namespace ProcSpector.Impl.Remote2
{
    internal static class Program
    {
        private static async Task Main()
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:8093");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                new HelloRequest { Name = "GreeterClient" });
            Console.WriteLine("Greeting: " + reply.Message);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}