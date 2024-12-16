namespace API.Dtos;

public class AuctionItemDto
{
    public AuctionItemDto(int itemId, string? title, DateTime? releaseDate, string? author, string? genre, string? description)
    {
        ItemID = itemId;
        Title = title;
        ReleaseDate = releaseDate;
        Author = author;
        Genre = genre;
        Description = description;
    }

    public int ItemID { get; init; }

    public string? Title { get; init; }

    public DateTime? ReleaseDate { get; init; }

    public string? Author { get; init; }

    public string? Genre { get; init; }

    public string? Description { get; init; }


}
