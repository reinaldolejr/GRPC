using Grpc.Core;
using GrpcStockService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwelveDataSharp;

namespace GrpcService.Services
{
    public class StockServiceImpl : StockService.StockServiceBase
    {
        public override async Task MonitorStocks(IAsyncStreamReader<StockRequest> requestStream, IServerStreamWriter<StockPrice> responseStream, ServerCallContext context)
        {
            var stockTickers = new HashSet<string>();
            var cts = context.CancellationToken;

            var receiveTask = Task.Run(async () =>
            {
                await foreach (var request in requestStream.ReadAllAsync(cts))
                {
                    if (stockTickers.Count >= 3)
                    {
                        await responseStream.WriteAsync(new StockPrice
                        {
                            Ticker = request.Ticker,
                            Price = 0,
                            Message = "Request refused: maximum of 3 stocks !!!"
                        });
                    }
                    else if (stockTickers.Any(t => t == request.Ticker))
                        stockTickers.Remove(request.Ticker);
                    else
                        stockTickers.Add(request.Ticker);
                    Console.WriteLine($"Add {request.Ticker}");
                }
            });

            var sendTask = Task.Run(async () =>
            {
                while (!cts.IsCancellationRequested)
                {
                    Console.WriteLine("Updated " + DateTime.UtcNow.ToString());
                    foreach (var ticker in stockTickers)
                    {
                        var stockPrice = await GetStockPriceFromAPI(ticker);

                        await responseStream.WriteAsync(new StockPrice
                        {
                            Ticker = ticker,
                            Price = stockPrice,
                            Message = "Updated " + DateTime.UtcNow.ToString()
                        });

                        Console.WriteLine($"{ticker} Price: {stockPrice}");
                    }

                    await Task.Delay(30 * 1000);
                }
            });

            await Task.WhenAll(receiveTask, sendTask);
        }

        private async Task<double> GetStockPriceFromAPI(string ticker)
        {
            var apiKey = "5952181e556f4168a1f0b734f60a52e9";
            var HttpClient = new HttpClient();
            var client = new TwelveDataClient(apiKey, HttpClient);

            var responseAPI = await client.GetRealTimePriceAsync(ticker);

            return responseAPI.Price;
        }
    }
}