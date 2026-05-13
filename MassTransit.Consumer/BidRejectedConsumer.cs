using MassTransit.Contract;
using System.Text.Json;

namespace MassTransit.Consumer
{
    internal class BidRejectedConsumer : IConsumer<BidRejected>
    {
        public Task Consume(ConsumeContext<BidRejected> context)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(context.Message, options);

            Console.WriteLine("Bid Rejected Consumed");

            Console.WriteLine(jsonString);

            return Task.CompletedTask;

        }
    }
}
