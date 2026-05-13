using MassTransit.Contract;
using System.Text.Json;

namespace MassTransit.Consumer
{
    internal class BidSubmittedConsume : IConsumer<BidSubmitted>
    {
        public async Task Consume(ConsumeContext<BidSubmitted> context)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(context.Message, options);

            Console.WriteLine("Bid Submitted Consumed");

            Console.WriteLine(jsonString);

            await context.Publish<BidAccepted>(new
            {
                Id = context.Message.Id,
                bid = context.Message.Bid
            });
        }
    }
}
