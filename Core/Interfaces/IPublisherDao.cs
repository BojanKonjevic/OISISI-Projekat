using Core.Models;

namespace Core.Interfaces;

public interface IPublisherDao
{
    List<Publisher> Load();
    void Save(List<Publisher> publishers);
}
