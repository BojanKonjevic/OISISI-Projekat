using Core.Interfaces;
using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class BookDao : IBookDao
{
    private static readonly string FilePath = DataStorage.PathFor("books.json");

    public List<Book> Load() => JsonFile<Book>.Load(FilePath);

    public void Save(List<Book> books) => JsonFile<Book>.Save(FilePath, books);

    public Book Add(Book book)
    {
        List<Book> books = Load();
        book.Id = books.Count == 0 ? 1 : books.Max(b => b.Id) + 1;
        books.Add(book);
        Save(books);
        return book;
    }
}
