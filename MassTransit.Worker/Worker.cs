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

                await CreateArtWork();

                await Task.Delay(10_000, stoppingToken);
            }
        }

        private async Task CreateArtWork()
        {
            var endpoint = await bus.GetSendEndpoint(new Uri("queue:create-art-work"));

            var blueQuran = new CreateArtWork
                  (
                    Name: "Folio from the Blue Qur'an",
                    Image: new Uri("https://images.metmuseum.org/CRDImages/is/original/DP167100.jpg"),
                    ArtistName: String.Empty,
                    LocationOrigin: "Made in Tunisia, possibly Qairawan"
                  );

            //You send to queue
            //but if you're sending to a topic that is subscribed to multiple consumers you publish
            await endpoint.Send(blueQuran);
        }
    }
}
