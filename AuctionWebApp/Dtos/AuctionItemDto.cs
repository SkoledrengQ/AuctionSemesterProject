namespace API.Dtos;

public class AuctionItemDto(int itemId, string? title, DateTime? releaseDate, string? author, string? genre, string? description)
{
    public int ItemID { get; init; } = itemId;

    public string? Title { get; init; } = title;

    public DateTime? ReleaseDate { get; init; } = releaseDate;

    public string? Author { get; init; } = author;

    public string? Genre { get; init; } = genre;

    public string? Description { get; init; } = description;


}
