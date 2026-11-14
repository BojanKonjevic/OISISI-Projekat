using Core.Models;

namespace Core.Interfaces;

public interface IBookDao
{
    List<Book> Load();
    void Save(List<Book> books);
}
