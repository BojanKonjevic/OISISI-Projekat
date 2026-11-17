namespace Core.Utils;

public class Serializer<T> where T : ISerializable, new()
{
    private const char Delimiter = '|';

    public string ToCSV(List<T> items)
    {
        System.Text.StringBuilder sb = new();
        foreach (T item in items)
        {
            sb.AppendLine(string.Join(Delimiter.ToString(), item.ToCSV()));
        }

        return sb.ToString();
    }

    public List<T> FromCSV(IEnumerable<string> lines)
    {
        List<T> items = [];
        foreach (string line in lines)
        {
            if (line.Length == 0)
            {
                continue;
            }

            T item = new();
            item.FromCSV(line.Split(Delimiter));
            items.Add(item);
        }

        return items;
    }
}
