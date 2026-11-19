using System.Globalization;
using Core.Utils;

namespace Core.Models;

public class Visitor : ISerializable
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
    public List<int> PurchaseIds { get; set; } = [];
    public List<int> WishlistBookIds { get; set; } = [];

    public override string ToString()
    {
        return $"Id: {Id} | Ime: {FirstName} | Prezime: {LastName} | Datum rodjenja: {DateOfBirth:yyyy-MM-dd} | Adresa: {AddressId} | Telefon: {Phone} | Email: {Email} | Br. clanske karte: {MembershipCardNumber} | Godina: {MembershipYear} | Status: {Status} | Ocena: {AverageRating} | Kupovine: [{string.Join(",", PurchaseIds)}] | Lista zelja: [{string.Join(",", WishlistBookIds)}]";
    }

    public string[] ToCSV()
    {
        return
        [
            Id.ToString(),
            FirstName,
            LastName,
            DateOfBirth.ToString("yyyy-MM-dd"),
            AddressId.ToString(),
            Phone,
            Email,
            MembershipCardNumber,
            MembershipYear.ToString(),
            Status.ToString(),
            AverageRating.ToString(CultureInfo.InvariantCulture),
            string.Join(",", PurchaseIds),
            string.Join(",", WishlistBookIds)
        ];
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        FirstName = values[1];
        LastName = values[2];
        DateOfBirth = DateOnly.ParseExact(values[3], "yyyy-MM-dd");
        AddressId = int.Parse(values[4]);
        Phone = values[5];
        Email = values[6];
        MembershipCardNumber = values[7];
        MembershipYear = int.Parse(values[8]);
        Status = Enum.Parse<MembershipStatus>(values[9]);
        AverageRating = double.Parse(values[10], CultureInfo.InvariantCulture);
        PurchaseIds = ParseIds(values[11]);
        WishlistBookIds = ParseIds(values[12]);
    }

    private static List<int> ParseIds(string value)
    {
        List<int> ids = [];
        foreach (string part in value.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            ids.Add(int.Parse(part));
        }

        return ids;
    }
}
