using Core.Models;

namespace Core.Interfaces;

public interface IAuthorDao
{
    List<Author> Load();
    void Save(List<Author> authors);
}
