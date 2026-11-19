using System.Globalization;
using Core.Utils;

namespace Core.Models;

public class Book : ISerializable
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

    public int ExpectedFieldCount => 11;

    public override string ToString()
    {
        return $"Id: {Id} | ISBN: {Isbn} | Naslov: {Title} | Zanr: {Genre} | Godina izdanja: {PublicationYear} | Cena: {Price} | Broj strana: {PageCount} | Autori: [{string.Join(",", AuthorIds)}] | Izdavac: {PublisherId} | Kupci: [{string.Join(",", BuyerVisitorIds)}] | Zelje: [{string.Join(",", WishlistVisitorIds)}]";
    }

    public string[] ToCSV()
    {
        return
        [
            Id.ToString(),
            Isbn,
            Title,
            Genre,
            PublicationYear.ToString(),
            Price.ToString(CultureInfo.InvariantCulture),
            PageCount.ToString(),
            string.Join(",", AuthorIds),
            PublisherId.ToString(),
            string.Join(",", BuyerVisitorIds),
            string.Join(",", WishlistVisitorIds)
        ];
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Isbn = values[1];
        Title = values[2];
        Genre = values[3];
        PublicationYear = int.Parse(values[4]);
        Price = decimal.Parse(values[5], CultureInfo.InvariantCulture);
        PageCount = int.Parse(values[6]);
        AuthorIds = ParseIds(values[7]);
        PublisherId = int.Parse(values[8]);
        BuyerVisitorIds = ParseIds(values[9]);
        WishlistVisitorIds = ParseIds(values[10]);
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
