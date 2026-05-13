using MassTransit.Contract;
using System.Text.Json;

namespace MassTransit.Consumer
{
    internal class BidRejectedConsume : IConsumer<BidRejected>
    {
        public  Task Consume(ConsumeContext<BidRejected> context)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(context.Message, options);

            Console.WriteLine("BidRejected Consumed");

            Console.WriteLine(jsonString);
      
            return Task.CompletedTask;

        }
    }
}
