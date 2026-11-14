namespace Core.Models;

public class Author
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public int AddressId { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string IdCardNumber { get; set; } = string.Empty;
    public int YearsOfExperience { get; set; }
    public List<int> BookIds { get; set; } = [];
}
