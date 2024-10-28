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
            // Mantenha o streaming ativo até que o cliente cancele
            while (!context.CancellationToken.IsCancellationRequested)
            {
                // Envia o horário atual como resposta
                var currentTime = DateTime.Now.ToLongTimeString();
                await responseStream.WriteAsync(new TimeReply { CurrentTime = currentTime });

                // Intervalo de 1 segundo entre as mensagens
                await Task.Delay(1000);
            }
        }
    }
}