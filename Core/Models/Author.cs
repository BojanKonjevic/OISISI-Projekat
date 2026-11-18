using Core.Utils;

namespace Core.Models;

public class Author : ISerializable
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

    public override string ToString()
    {
        return $"Id: {Id} | Ime: {FirstName} | Prezime: {LastName} | Datum rodjenja: {DateOfBirth:yyyy-MM-dd} | Adresa: {AddressId} | Telefon: {Phone} | Email: {Email} | Licna: {IdCardNumber} | Godine iskustva: {YearsOfExperience} | Knjige: [{string.Join(",", BookIds)}]";
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
            IdCardNumber,
            YearsOfExperience.ToString(),
            string.Join(",", BookIds)
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
        IdCardNumber = values[7];
        YearsOfExperience = int.Parse(values[8]);
        BookIds = ParseIds(values[9]);
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
