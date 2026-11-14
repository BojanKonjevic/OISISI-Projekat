namespace Core.Utils;

public static class DataStorage
{
    public static string PathFor(string fileName)
    {
        string directory = Path.Combine(AppContext.BaseDirectory, "Data");
        return $"{directory}{Path.DirectorySeparatorChar}{fileName}";
    }
}
