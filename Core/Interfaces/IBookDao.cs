using Core.Models;

namespace Core.Interfaces;

public interface IBookDao
{
    Book Add(Book book);

    List<Book> GetAll();
}
