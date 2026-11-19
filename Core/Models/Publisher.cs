using Core.Utils;

namespace Core.Models;

public class Publisher : ISerializable
{
    public const int MinHeadAuthorExperience = 5;

    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int? HeadAuthorId { get; set; }
    public List<int> AuthorIds { get; set; } = [];
    public List<int> BookIds { get; set; } = [];

    public override string ToString()
    {
        return $"Id: {Id} | Sifra: {Code} | Naziv: {Name} | Sef: {HeadAuthorId?.ToString() ?? "-"} | Autori: [{string.Join(",", AuthorIds)}] | Knjige: [{string.Join(",", BookIds)}]";
    }

    public string[] ToCSV()
    {
        return
        [
            Id.ToString(),
            Code,
            Name,
            HeadAuthorId?.ToString() ?? string.Empty,
            string.Join(",", AuthorIds),
            string.Join(",", BookIds)
        ];
    }

    public void FromCSV(string[] values)
    {
        Id = int.Parse(values[0]);
        Code = values[1];
        Name = values[2];
        HeadAuthorId = values[3].Length == 0 ? null : int.Parse(values[3]);
        AuthorIds = ParseIds(values[4]);
        BookIds = ParseIds(values[5]);
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
