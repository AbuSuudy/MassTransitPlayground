namespace MassTransit.RoutingSlip
{
    public class AquiredArguments
    {
        public Guid Id { get; init; }

        public decimal Bid { get; init; }

        public decimal Price { get; init; }
    }

    public class AquiredLog
    {
        public Guid Id { get; init; }

        public decimal Bid { get; init; }

        public decimal Price { get; init; }
    }

    public class AquiredActivity() : IActivity<AquiredArguments, AquiredLog>
    {
        public async Task<CompensationResult> Compensate(CompensateContext<AquiredLog> context)
        {
            Console.WriteLine("Aquired Compensared");

            return context.Compensated();
        }

        async Task<ExecutionResult> IExecuteActivity<AquiredArguments>.Execute(ExecuteContext<AquiredArguments> context)
        {

            Console.WriteLine($"Aquired - Id : {context.Arguments.Id} Bid: {context.Arguments.Bid} Price: {context.Arguments.Price}");


            return context.Completed<AquiredLog>(new
            {
                Bid = context.Arguments.Bid,
                Id = context.Arguments.Id,
                Price = context.Arguments.Price,
            });
        }
    }
}
