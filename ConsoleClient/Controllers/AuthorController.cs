using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers;

public class AuthorController
{
    private readonly AuthorDao _authorDao;
    private readonly AddressDao _addressDao;
    private readonly BookDao _bookDao;

    public AuthorController(AuthorDao authorDao, AddressDao addressDao, BookDao bookDao)
    {
        _authorDao = authorDao;
        _addressDao = addressDao;
        _bookDao = bookDao;
    }

    public void AddAuthor()
    {
        Console.WriteLine("--- Dodavanje autora ---");
        Author author = ReadAuthor();
        _authorDao.Add(author);
        Console.WriteLine($"Autor je dodat sa Id {author.Id}.");
    }

    public void EditAuthor()
    {
        Console.WriteLine("--- Izmena autora ---");
        int id = ConsoleInput.ReadExistingId("Id autora", id => _authorDao.GetAll().Any(a => a.Id == id));
        Author author = ReadAuthor(id);
        _authorDao.Update(author);
        Console.WriteLine("Autor je izmenjen.");
    }

    public void DeleteAuthor()
    {
        Console.WriteLine("--- Brisanje autora ---");
        int id = ConsoleInput.ReadExistingId("Id autora", id => _authorDao.GetAll().Any(a => a.Id == id));
        _authorDao.Remove(id);
        Console.WriteLine("Autor je obrisan.");
    }

    private Author ReadAuthor(int id = 0)
    {
        return new Author()
        {
            Id = id,
            FirstName = ConsoleInput.ReadText("Ime"),
            LastName = ConsoleInput.ReadText("Prezime"),
            DateOfBirth = ConsoleInput.ReadDate("Datum rodjenja", new DateOnly(1900, 1, 1), DateOnly.FromDateTime(DateTime.Today)),
            AddressId = ConsoleInput.ReadExistingId("Id adrese", id => _addressDao.GetAll().Any(a => a.Id == id)),
            Phone = ConsoleInput.ReadPhone("Telefon"),
            Email = ConsoleInput.ReadEmail("Email"),
            IdCardNumber = ConsoleInput.ReadText("Broj licne karte"),
            YearsOfExperience = ConsoleInput.ReadInt("Godine iskustva"),
            BookIds = ConsoleInput.ReadExistingIds("Id-evi knjiga", id => _bookDao.GetAll().Any(b => b.Id == id))
        };
    }
}
