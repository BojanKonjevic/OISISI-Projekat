using Core.Interfaces;
using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class PublisherDao : IPublisherDao
{
    private static readonly string FilePath = DataStorage.PathFor("publishers.json");

    public List<Publisher> Load() => JsonFile<Publisher>.Load(FilePath);

    public void Save(List<Publisher> publishers) => JsonFile<Publisher>.Save(FilePath, publishers);

    public Publisher Add(Publisher publisher)
    {
        List<Publisher> publishers = Load();
        publisher.Id = publishers.Count == 0 ? 1 : publishers.Max(p => p.Id) + 1;
        publishers.Add(publisher);
        Save(publishers);
        return publisher;
    }
}
