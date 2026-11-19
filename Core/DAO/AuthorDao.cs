using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class AuthorDao
{
    private readonly List<Author> _authors;
    private readonly Storage<Author> _storage;

    public AuthorDao()
    {
        _storage = new Storage<Author>("authors.csv");
        _authors = _storage.Load();
    }

    public Author Add(Author author)
    {
        author.Id = _authors.Count == 0 ? 1 : _authors.Max(a => a.Id) + 1;
        _authors.Add(author);
        _storage.Save(_authors);
        return author;
    }

    public Author? Update(Author author)
    {
        int index = _authors.FindIndex(a => a.Id == author.Id);
        if (index < 0)
        {
            return null;
        }

        _authors[index] = author;
        _storage.Save(_authors);
        return author;
    }

    public Author? Remove(int id)
    {
        Author? author = _authors.Find(a => a.Id == id);
        if (author is null)
        {
            return null;
        }

        _authors.Remove(author);
        _storage.Save(_authors);
        return author;
    }

    public List<Author> GetAll() => _authors;
}
