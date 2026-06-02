namespace MassTransit.RoutingSlip
{
    public class BidRejectedArguments
    {
        public Guid Id { get; init; }

        public decimal Bid { get; init; }

        public decimal Price { get; init; }
    }

    public class BidRejectedLog
    {
        public Guid Id { get; init; }

        public decimal Bid { get; init; }

        public decimal Price { get; init; }
    }

    public class BidRejectedActivity() : IActivity<BidRejectedArguments, BidRejectedLog>

    {
        public async Task<CompensationResult> Compensate(CompensateContext<BidRejectedLog> context)
        {
            Console.WriteLine("Bid Rejected Compensared");

            return context.Compensated();
        }

        async Task<ExecutionResult> IExecuteActivity<BidRejectedArguments>.Execute(ExecuteContext<BidRejectedArguments> context)
        {
            Console.WriteLine($"Rejected - Id : {context.Arguments.Id} Bid: {context.Arguments.Bid} Price: {context.Arguments.Price}");

            return context.Completed<BidRejectedLog>(new
            {
                Bid = context.Arguments.Bid,
                Id = context.Arguments.Id,
                Price = context.Arguments.Price,
            });
        }
    }
}
