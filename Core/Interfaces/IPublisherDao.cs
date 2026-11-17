using Core.Models;

namespace Core.Interfaces;

public interface IPublisherDao
{
    Publisher Add(Publisher publisher);

    Publisher? Update(Publisher publisher);

    List<Publisher> GetAll();
}
