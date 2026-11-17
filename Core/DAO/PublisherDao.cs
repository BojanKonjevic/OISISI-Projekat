using Core.Interfaces;
using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class PublisherDao : IPublisherDao
{
    private readonly List<Publisher> _publishers;
    private readonly Storage<Publisher> _storage;

    public PublisherDao()
    {
        _storage = new Storage<Publisher>("publishers.csv");
        _publishers = _storage.Load();
    }

    public Publisher Add(Publisher publisher)
    {
        publisher.Id = _publishers.Count == 0 ? 1 : _publishers.Max(p => p.Id) + 1;
        _publishers.Add(publisher);
        _storage.Save(_publishers);
        return publisher;
    }

    public Publisher? Update(Publisher publisher)
    {
        int index = _publishers.FindIndex(p => p.Id == publisher.Id);
        if (index < 0)
        {
            return null;
        }

        _publishers[index] = publisher;
        _storage.Save(_publishers);
        return publisher;
    }

    public List<Publisher> GetAll() => _publishers;
}
