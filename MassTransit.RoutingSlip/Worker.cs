using MassTransit.Contract;

namespace MassTransit.RoutingSlip
{
    public class Worker(ILogger<Worker> logger, IBus bus) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (logger.IsEnabled(LogLevel.Information))
                {
                    logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                await CreateBid();

                await Task.Delay(10_000_000, stoppingToken);
            }
        }

        private async Task CreateBid()
        {
            Random rnd = new Random();

            var blueQuran = new BidSubmitted
            {
                Id = Guid.NewGuid(),
                Bid = rnd.Next(50_000, 150_000),
                Price = 100_000
            };

            await bus.Publish<BidSubmitted>(blueQuran);
        }
    }
}

