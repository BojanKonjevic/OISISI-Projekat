using Core.Interfaces;
using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class AuthorDao : IAuthorDao
{
    private static readonly string FilePath = DataStorage.PathFor("authors.json");

    public List<Author> Load() => JsonFile<Author>.Load(FilePath);

    public void Save(List<Author> authors) => JsonFile<Author>.Save(FilePath, authors);
}
