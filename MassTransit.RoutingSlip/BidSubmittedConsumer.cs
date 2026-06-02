using MassTransit.Contract;

namespace MassTransit.RoutingSlip
{
    public class BidSubmittedConsumer : IConsumer<BidSubmitted>
    {
        public async Task Consume(ConsumeContext<BidSubmitted> context)
        {
            Console.WriteLine($"Bid Submitted - Id : {context.Message.Id} Bid: {context.Message.Bid} Price: {context.Message.Price}");

            var builder = new RoutingSlipBuilder(NewId.NextGuid());

            if (context.Message.Bid >= context.Message.Price * 0.8m)
            {
                builder.AddActivity("BidAccepted", new Uri("queue:bidaccepted_execute"), new
                {
                    Id = context.Message.Id,
                    bid = context.Message.Bid,
                    Price = context.Message.Price
                });

                builder.AddActivity("Aquired", new Uri("queue:aquired_execute") , new
                {
                    Id = context.Message.Id,
                    bid = context.Message.Bid,
                    Price = context.Message.Price
                });

            }
            else
            {
                builder.AddActivity("BidRejected", new Uri("queue:bidrejected_execute"), new
                {
                    Id = context.Message.Id,
                    bid = context.Message.Bid,
                    Price = context.Message.Price
                });

            }

            var routingSlipBuild = builder.Build();

            await context.Execute(routingSlipBuild).ConfigureAwait(false);
        }
    }
}
