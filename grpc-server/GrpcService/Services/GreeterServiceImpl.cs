using Grpc.Core;
using GrpcService;

namespace GrpcService.Services;

public class GreeterServiceImpl : GreeterService.GreeterServiceBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}