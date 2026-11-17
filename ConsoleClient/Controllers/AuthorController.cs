using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers;

public class AuthorController
{
    private readonly AuthorDao _authorDao;
    private readonly AddressDao _addressDao;

    public AuthorController(AuthorDao authorDao, AddressDao addressDao)
    {
        _authorDao = authorDao;
        _addressDao = addressDao;
    }

    public void AddAuthor()
    {
        Console.WriteLine("--- Dodavanje autora ---");

        Author author = new()
        {
            FirstName = ConsoleInput.ReadText("Ime"),
            LastName = ConsoleInput.ReadText("Prezime"),
            DateOfBirth = ConsoleInput.ReadDate("Datum rodjenja", new DateOnly(1900, 1, 1), DateOnly.FromDateTime(DateTime.Today)),
            AddressId = ConsoleInput.ReadExistingId("Id adrese", id => _addressDao.GetAll().Any(a => a.Id == id)),
            Phone = ConsoleInput.ReadPhone("Telefon"),
            Email = ConsoleInput.ReadEmail("Email"),
            IdCardNumber = ConsoleInput.ReadText("Broj licne karte"),
            YearsOfExperience = ConsoleInput.ReadInt("Godine iskustva"),
            BookIds = ConsoleInput.ReadIds("Id-evi knjiga")
        };

        _authorDao.Add(author);
        Console.WriteLine($"Autor je dodat sa Id {author.Id}.");
    }
}
