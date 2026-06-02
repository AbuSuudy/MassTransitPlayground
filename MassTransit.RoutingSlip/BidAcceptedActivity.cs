namespace MassTransit.RoutingSlip
{
    public class BidAcceptedArguments
    {
        public Guid Id { get; init; }

        public decimal Bid { get; init; }

        public decimal Price { get; init; }
    }

    public class BidAcceptedLog
    {
        public Guid Id { get; init; }

        public decimal Bid { get; init; }

        public decimal Price { get; init; }
    }

    public class BidAcceptedActivity() : IActivity<BidAcceptedArguments, BidAcceptedLog>

    {
        public async Task<CompensationResult> Compensate(CompensateContext<BidAcceptedLog> context)
        {
            Console.WriteLine("Bid Accepted Compensared");

            return context.Compensated();
        }

        async Task<ExecutionResult> IExecuteActivity<BidAcceptedArguments>.Execute(ExecuteContext<BidAcceptedArguments> context)
        {
            Console.WriteLine($"Bid Accpeted - Id : {context.Arguments.Id} Bid: {context.Arguments.Bid} Price: {context.Arguments.Price}");

            return context.Completed<BidAcceptedLog>(new
            {
                Bid = context.Arguments.Bid,
                Id = context.Arguments.Id,
                Price = context.Arguments.Price,
            });
        }
    }
}
