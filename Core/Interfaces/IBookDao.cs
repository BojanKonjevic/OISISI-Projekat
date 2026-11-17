using Core.Models;

namespace Core.Interfaces;

public interface IBookDao
{
    Book Add(Book book);

    Book? Update(Book book);

    Book? Remove(int id);

    List<Book> GetAll();
}
