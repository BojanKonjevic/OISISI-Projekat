using System.Text.Json;

namespace Core.Utils;

public static class JsonFile<T>
{
    private static readonly JsonSerializerOptions Options = new() { WriteIndented = true };

    public static List<T> Load(string path)
    {
        if (!File.Exists(path))
        {
            return [];
        }

        string json = File.ReadAllText(path);
        return JsonSerializer.Deserialize<List<T>>(json, Options) ?? [];
    }

    public static void Save(string path, List<T> items)
    {
        string? directory = Path.GetDirectoryName(path);
        if (directory is not null)
        {
            Directory.CreateDirectory(directory);
        }

        File.WriteAllText(path, JsonSerializer.Serialize(items, Options));
    }
}
