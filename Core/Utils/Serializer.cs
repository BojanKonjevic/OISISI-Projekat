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

    public List<T> FromCSV(IEnumerable<string> lines, string? filePath = null)
    {
        List<T> items = [];
        int lineNumber = 0;
        foreach (string line in lines)
        {
            lineNumber++;
            if (line.Length == 0)
            {
                continue;
            }

            string[] values = line.Split(Delimiter);
            T item = new();
            if (values.Length != item.ExpectedFieldCount)
            {
                throw new DataFileException(
                    filePath,
                    lineNumber,
                    $"ocekivano {item.ExpectedFieldCount} polja, pronadjeno {values.Length}");
            }

            try
            {
                item.FromCSV(values);
            }
            catch (Exception ex) when (ex is FormatException or OverflowException or ArgumentException or IndexOutOfRangeException)
            {
                throw new DataFileException(filePath, lineNumber, $"neispravna vrednost ({ex.Message})", ex);
            }

            items.Add(item);
        }

        return items;
    }
}
