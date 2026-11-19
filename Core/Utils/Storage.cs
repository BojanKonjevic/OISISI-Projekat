namespace Core.Utils;

public class Storage<T> where T : ISerializable, new()
{
    private readonly string _filePath;
    private readonly Serializer<T> _serializer = new();

    public Storage(string fileName)
    {
        _filePath = DataStorage.PathFor(fileName);
    }

    public List<T> Load()
    {
        if (!File.Exists(_filePath))
        {
            string? directory = Path.GetDirectoryName(_filePath);
            if (directory is not null)
            {
                Directory.CreateDirectory(directory);
            }

            File.Create(_filePath).Close();
            return [];
        }

        return _serializer.FromCSV(File.ReadLines(_filePath), _filePath);
    }

    public void Save(List<T> items)
    {
        string? directory = Path.GetDirectoryName(_filePath);
        if (directory is not null)
        {
            Directory.CreateDirectory(directory);
        }

        using StreamWriter writer = new(_filePath);
        writer.Write(_serializer.ToCSV(items));
    }
}
