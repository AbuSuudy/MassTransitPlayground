using MassTransit.Contract;
using System.Text.Json;

namespace MassTransit.Consumer
{
    internal class CreateArtWorkConsumer : IConsumer<CreateArtWork>
    {
        public Task Consume(ConsumeContext<CreateArtWork> context)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(context.Message, options);

            Console.WriteLine(jsonString);

            return Task.CompletedTask;
        }
    }
}
