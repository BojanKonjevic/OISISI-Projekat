namespace Core.Utils;

public static class DataStorage
{
    private const string DataFolderName = "Data";

    public static string PathFor(string fileName)
    {
        return $"{FindDataFolder()}{Path.DirectorySeparatorChar}{fileName}";
    }

    private static string FindDataFolder()
    {
        DirectoryInfo? directory = new DirectoryInfo(AppContext.BaseDirectory);
        while (directory is not null)
        {
            if (directory.EnumerateFiles("*.sln").Any())
            {
                return $"{directory.FullName}{Path.DirectorySeparatorChar}{DataFolderName}";
            }

            directory = directory.Parent;
        }

        return $"{AppContext.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar)}{Path.DirectorySeparatorChar}{DataFolderName}";
    }
}
