using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers;

public class PublisherController
{
    private readonly PublisherDao _publisherDao;
    private readonly AuthorDao _authorDao;
    private readonly BookDao _bookDao;

    public PublisherController(PublisherDao publisherDao, AuthorDao authorDao, BookDao bookDao)
    {
        _publisherDao = publisherDao;
        _authorDao = authorDao;
        _bookDao = bookDao;
    }

    public void AddPublisher()
    {
        Console.WriteLine("--- Dodavanje izdavaca ---");

        Publisher publisher = new()
        {
            Code = ConsoleInput.ReadUniqueText("Sifra", _publisherDao.GetAll().Select(p => p.Code)),
            Name = ConsoleInput.ReadText("Naziv"),
            HeadAuthorId = ReadHeadAuthorId(),
            AuthorIds = ConsoleInput.ReadExistingIds("Id-evi autora", id => _authorDao.GetAll().Any(a => a.Id == id)),
            BookIds = ConsoleInput.ReadExistingIds("Id-evi knjiga", id => _bookDao.GetAll().Any(b => b.Id == id))
        };

        _publisherDao.Add(publisher);
        Console.WriteLine($"Izdavac je dodat sa Id {publisher.Id}.");
    }

    private static int? ReadHeadAuthorId()
    {
        Console.Write("Id sefa izdavaca (prazno za preskakanje): ");
        string input = Console.ReadLine()?.Trim() ?? string.Empty;
        if (input.Length == 0)
        {
            return null;
        }

        if (int.TryParse(input, out int id))
        {
            return id;
        }

        Console.WriteLine("Unet je neispravan Id, ostavlja se prazno.");
        return null;
    }
}
