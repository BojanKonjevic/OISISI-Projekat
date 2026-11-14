using Core.Interfaces;
using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class BookDao : IBookDao
{
    private static readonly string FilePath = DataStorage.PathFor("books.json");

    public List<Book> Load() => JsonFile<Book>.Load(FilePath);

    public void Save(List<Book> books) => JsonFile<Book>.Save(FilePath, books);
}
