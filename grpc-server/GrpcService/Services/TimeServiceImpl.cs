using System;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcTimeService;

namespace GrpcService.Services
{
    public class TimeServiceImpl : TimeService.TimeServiceBase
    {
        public override async Task GetServerTime(TimeRequest request, IServerStreamWriter<TimeReply> responseStream, ServerCallContext context)
        {
            while (!context.CancellationToken.IsCancellationRequested)
            {
                var currentTime = DateTime.Now.ToLongTimeString();
                await responseStream.WriteAsync(new TimeReply { CurrentTime = currentTime });

                await Task.Delay(1000);
            }
        }
    }
}