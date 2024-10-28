using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using GrpcService;
using GrpcTimeService;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7294");
            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(new HelloRequest { Name = "Reinaldo" });
            Console.WriteLine("Greeting: " + reply.Message);

            var clientTime = new TimeService.TimeServiceClient(channel);

            using var streamingCall = clientTime.GetServerTime(new TimeRequest { ClientId = "Client1" });

            try
            {
                await foreach (var response in streamingCall.ResponseStream.ReadAllAsync(CancellationToken.None))
                {
                    Console.WriteLine("Horario: " + response.CurrentTime);
                }
            }
            catch (RpcException e) when (e.StatusCode == StatusCode.Cancelled)
            {
                Console.WriteLine("Stream cancelado.");
            }
        }
    }
}