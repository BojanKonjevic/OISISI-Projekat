using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class BookDao
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

    public Book? Update(Book book)
    {
        int index = _books.FindIndex(b => b.Id == book.Id);
        if (index < 0)
        {
            return null;
        }

        _books[index] = book;
        _storage.Save(_books);
        return book;
    }

    public Book? Remove(int id)
    {
        Book? book = _books.Find(b => b.Id == id);
        if (book is null)
        {
            return null;
        }

        _books.Remove(book);
        _storage.Save(_books);
        return book;
    }

    public List<Book> GetAll() => _books;
}
