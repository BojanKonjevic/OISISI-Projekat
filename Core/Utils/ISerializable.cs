namespace Core.Utils;

public interface ISerializable
{
    string[] ToCSV();

    void FromCSV(string[] values);
}
