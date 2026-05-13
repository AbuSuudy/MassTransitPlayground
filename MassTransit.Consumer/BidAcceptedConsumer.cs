using MassTransit.Contract;
using System.Text.Json;

namespace MassTransit.Consumer
{
    internal class BidAcceptedConsumer : IConsumer<BidAccepted>
    {
        public Task Consume(ConsumeContext<BidAccepted> context)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(context.Message, options);

            Console.WriteLine("BidAccepted Consumed");

            Console.WriteLine(jsonString);

            return Task.CompletedTask;

        }
    }
}
