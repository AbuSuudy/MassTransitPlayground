using MassTransit;

namespace MassTransit.Contract
{
    public record BidSubmitted
    {
        public Guid Id { get; init; }

        public decimal Bid { get; init; }

        public decimal Price { get; init; }
    }

    public interface BidAccepted : CorrelatedBy<Guid>
    {
        public Guid Id { get; init; }

        public decimal Bid { get; init; }
    }

    public interface BidRejected : CorrelatedBy<Guid>
    {
        public Guid Id { get; init; }
    }

}
