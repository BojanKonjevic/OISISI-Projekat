using Core.Models;

namespace Core.Interfaces;

public interface IAuthorDao
{
    Author Add(Author author);

    Author? Update(Author author);

    Author? Remove(int id);

    List<Author> GetAll();
}
