using Core.Utils;

namespace Core.Models;

public class Purchase : ISerializable
{
    public int Id { get; set; }
    public int VisitorId { get; set; }
    public int BookId { get; set; }
    public DateOnly PurchaseDate { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"Id: {Id} | Posetilac: {VisitorId} | Knjiga: {BookId} | Datum kupovine: {PurchaseDate:yyyy-MM-dd} | Ocena: {Rating} | Komentar: {Comment}";
    }

    public string[] ToCSV()
    {
        return
        [
            Id.ToString(),
            VisitorId.ToString(),
            BookId.ToString(),
            PurchaseDate.ToString("yyyy-MM-dd"),
            Rating.ToString(),
            Comment
        ];
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        VisitorId = int.Parse(values[1]);
        BookId = int.Parse(values[2]);
        PurchaseDate = DateOnly.ParseExact(values[3], "yyyy-MM-dd");
        Rating = int.Parse(values[4]);
        Comment = values[5];
    }
}
