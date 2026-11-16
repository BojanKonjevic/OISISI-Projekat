using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers;

public class AuthorController
{
    private readonly AuthorDao _authorDao;

    public AuthorController(AuthorDao authorDao)
    {
        _authorDao = authorDao;
    }

    public void AddAuthor()
    {
        Console.WriteLine("--- Dodavanje autora ---");

        Author author = new()
        {
            FirstName = ConsoleInput.ReadText("Ime"),
            LastName = ConsoleInput.ReadText("Prezime"),
            DateOfBirth = ConsoleInput.ReadDate("Datum rodjenja"),
            AddressId = ConsoleInput.ReadInt("Id adrese"),
            Phone = ConsoleInput.ReadText("Telefon"),
            Email = ConsoleInput.ReadText("Email"),
            IdCardNumber = ConsoleInput.ReadText("Broj licne karte"),
            YearsOfExperience = ConsoleInput.ReadInt("Godine iskustva"),
            BookIds = ConsoleInput.ReadIds("Id-evi knjiga")
        };

        _authorDao.Add(author);
        Console.WriteLine($"Autor je dodat sa Id {author.Id}.");
    }
}
