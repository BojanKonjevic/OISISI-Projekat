namespace Core.Models;

public class Visitor
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public int AddressId { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MembershipCardNumber { get; set; } = string.Empty;
    public int MembershipYear { get; set; }
    public MembershipStatus Status { get; set; }
    public double AverageRating { get; set; }
    public List<int> PurchasedBookIds { get; set; } = [];
    public List<int> WishlistBookIds { get; set; } = [];
}
