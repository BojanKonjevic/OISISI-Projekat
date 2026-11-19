namespace Core.Utils;

public class DataFileException : Exception
{
    public DataFileException(string? filePath, int lineNumber, string details, Exception? innerException = null)
        : base(BuildMessage(filePath, lineNumber, details), innerException)
    {
        FilePath = filePath;
        LineNumber = lineNumber;
    }

    public string? FilePath { get; }

    public int LineNumber { get; }

    private static string BuildMessage(string? filePath, int lineNumber, string details)
    {
        string location = string.IsNullOrWhiteSpace(filePath)
            ? $"na liniji {lineNumber}"
            : $"u fajlu '{Path.GetFileName(filePath)}' na liniji {lineNumber}";
        return $"Neispravan zapis {location}: {details.TrimEnd('.')}.";
    }
}
