using Core.Utils;

namespace Core.Models;

public class Address : ISerializable
{
    public int Id { get; set; }
    public string Street { get; set; } = string.Empty;
    public string Number { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"Id: {Id} | Ulica: {Street} | Broj: {Number} | Grad: {City} | Drzava: {Country}";
    }

    public string[] ToCSV()
    {
        return
        [
            Id.ToString(),
            Street,
            Number,
            City,
            Country
        ];
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Street = values[1];
        Number = values[2];
        City = values[3];
        Country = values[4];
    }
}
