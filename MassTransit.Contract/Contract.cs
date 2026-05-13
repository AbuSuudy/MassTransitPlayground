namespace MassTransit.Contract
{


    public record BidSubmitted
    {

        public Guid Id { get; init; }

        public decimal Bid { get; init; }

        public decimal Price { get; init; }
    }


    public record BidAccepted
    {
        public Guid Id { get; init; }

        public decimal Bid { get; init; }
    }

    public record BidRejected
    {
        public Guid Id { get; init; }

    }

}
