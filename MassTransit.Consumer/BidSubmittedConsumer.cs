using MassTransit.Contract;
using System.Text.Json;

namespace MassTransit.Consumer
{
    internal class BidSubmittedConsumer : IConsumer<BidSubmitted>
    {
        public async Task Consume(ConsumeContext<BidSubmitted> context)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(context.Message, options);

            Console.WriteLine("Bid Submitted Consumed");

            Console.WriteLine(jsonString);

            //Accept bid greater than 80% of the price
            if (context.Message.Bid >= context.Message.Price * 0.8m)
            {
                await context.Publish<BidAccepted>(new
                {
                    Id = context.Message.Id,
                    bid = context.Message.Bid
                });
            }
            else
            {
                await context.Publish<BidRejected>(new
                {
                    Id = context.Message.Id,
                });
            }


        }
    }
}
