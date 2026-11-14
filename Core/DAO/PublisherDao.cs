using Core.Interfaces;
using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class PublisherDao : IPublisherDao
{
    private static readonly string FilePath = DataStorage.PathFor("publishers.json");

    public List<Publisher> Load() => JsonFile<Publisher>.Load(FilePath);

    public void Save(List<Publisher> publishers) => JsonFile<Publisher>.Save(FilePath, publishers);
}
