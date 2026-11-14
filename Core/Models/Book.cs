namespace Core.Models;

public class Book
{
    public int Id { get; set; }
    public string Isbn { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public int PublicationYear { get; set; }
    public decimal Price { get; set; }
    public int PageCount { get; set; }
    public List<int> AuthorIds { get; set; } = [];
    public int PublisherId { get; set; }
    public List<int> BuyerVisitorIds { get; set; } = [];
    public List<int> WishlistVisitorIds { get; set; } = [];
}
