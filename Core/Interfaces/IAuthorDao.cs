using Core.Models;

namespace Core.Interfaces;

public interface IAuthorDao
{
    Author Add(Author author);

    List<Author> GetAll();
}
