namespace Core.Utils;

public interface ISerializable
{
    int ExpectedFieldCount { get; }

    string[] ToCSV();

    void FromCSV(string[] values);
}
