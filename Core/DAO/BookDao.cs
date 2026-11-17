using Core.Interfaces;
using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class BookDao : IBookDao
{
    private readonly List<Book> _books;
    private readonly Storage<Book> _storage;

    public BookDao()
    {
        _storage = new Storage<Book>("books.csv");
        _books = _storage.Load();
    }

    public Book Add(Book book)
    {
        book.Id = _books.Count == 0 ? 1 : _books.Max(b => b.Id) + 1;
        _books.Add(book);
        _storage.Save(_books);
        return book;
    }

    public List<Book> GetAll() => _books;
}
