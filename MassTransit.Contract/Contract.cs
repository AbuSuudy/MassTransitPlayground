namespace MassTransit.Contract
{
    public record CreateArtWork(string Name, Uri Image, string ArtistName, string LocationOrigin);
}
