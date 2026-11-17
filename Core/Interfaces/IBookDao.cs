using Core.Models;

namespace Core.Interfaces;

public interface IBookDao
{
    Book Add(Book book);

    Book? Update(Book book);

    List<Book> GetAll();
}
