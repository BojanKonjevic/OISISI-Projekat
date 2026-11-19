using Core.DAO;
using Core.Models;
using Core.Services;

namespace ConsoleClient.Controllers;

public class AuthorController
{
    private readonly AuthorDao _authorDao;
    private readonly AddressDao _addressDao;
    private readonly BookDao _bookDao;
    private readonly PublisherDao _publisherDao;
    private readonly ReferenceSyncService _referenceSync;

    public AuthorController(AuthorDao authorDao, AddressDao addressDao, BookDao bookDao, PublisherDao publisherDao, ReferenceSyncService referenceSync)
    {
        _authorDao = authorDao;
        _addressDao = addressDao;
        _bookDao = bookDao;
        _publisherDao = publisherDao;
        _referenceSync = referenceSync;
    }

    public void AddAuthor()
    {
        Console.WriteLine("--- Dodavanje autora ---");
        Author author = ReadAuthor();
        _authorDao.Add(author);
        ConsoleOutput.Success($"Autor je dodat sa Id {author.Id}.");
    }

    public void EditAuthor()
    {
        Console.WriteLine("--- Izmena autora ---");
        int id = ConsoleInput.ReadExistingId("Id autora", id => _authorDao.GetAll().Any(a => a.Id == id));
        Author author = ReadAuthor(id);
        _authorDao.Update(author);
        _referenceSync.SyncAll();
        ConsoleOutput.Success("Autor je izmenjen.");
    }

    public void ShowAuthors()
    {
        ConsoleOutput.Header("\n===== AUTORI =====");
        foreach (Author author in _authorDao.GetAll())
        {
            Console.WriteLine(author);
            ConsoleOutput.Divider();
        }
    }

    public void DeleteAuthor()
    {
        Console.WriteLine("--- Brisanje autora ---");
        int id = ConsoleInput.ReadExistingId("Id autora", id => _authorDao.GetAll().Any(a => a.Id == id));
        if (_bookDao.GetAll().Any(b => b.AuthorIds.Contains(id) && b.AuthorIds.Count == 1))
        {
            ConsoleOutput.Error("Autor ne moze biti obrisan jer je jedini autor knjige.");
            return;
        }

        if (_publisherDao.GetAll().Any(p => p.HeadAuthorId == id || p.AuthorIds.Contains(id)))
        {
            ConsoleOutput.Error("Autor ne moze biti obrisan jer ga referenciraju izdavaci.");
            return;
        }

        _authorDao.Remove(id);
        _referenceSync.SyncAll();
        ConsoleOutput.Success("Autor je obrisan.");
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
            YearsOfExperience = ConsoleInput.ReadInt("Godine iskustva")
        };
    }
}
