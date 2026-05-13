using MassTransit.Contract;
namespace MassTransit.Worker
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

                await Task.Delay(10_000, stoppingToken);
            }
        }

        private async Task CreateBid()
        {
            var blueQuran = new BidSubmitted
            {
                Id = Guid.NewGuid(),
                Bid = 20_00
            };

            await bus.Publish<BidSubmitted>(blueQuran);
        }
    }
}
