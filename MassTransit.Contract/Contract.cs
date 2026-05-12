namespace MassTransit.Contract
{
    public record CreateArtWork(string Name, Uri Image, string ArtistName, string LocationOrigin);

    public record BidSubmitted(Guid Id, decimal Bid);

    public record BidAccepted(Guid Id, decimal Bid);

    public record BidRejected(Guid Id, decimal Bid);

    public record CounterOffer(Guid Id, decimal bid, decimal Offer);

    public record CounterOfferAccepted(Guid Id, decimal Offer);

    public record CounterOfferRejected(Guid Id, decimal Offer);

}
