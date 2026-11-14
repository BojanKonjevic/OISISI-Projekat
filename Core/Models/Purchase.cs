namespace Core.Models;

public class Purchase
{
    public int Id { get; set; }
    public int VisitorId { get; set; }
    public int BookId { get; set; }
    public DateOnly PurchaseDate { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
}
