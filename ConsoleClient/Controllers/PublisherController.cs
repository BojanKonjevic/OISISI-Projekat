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
        Publisher publisher = ReadPublisher();
        _publisherDao.Add(publisher);
        Console.WriteLine($"Izdavac je dodat sa Id {publisher.Id}.");
    }

    public void EditPublisher()
    {
        Console.WriteLine("--- Izmena izdavaca ---");
        int id = ConsoleInput.ReadExistingId("Id izdavaca", id => _publisherDao.GetAll().Any(p => p.Id == id));
        Publisher publisher = ReadPublisher(id);
        _publisherDao.Update(publisher);
        Console.WriteLine("Izdavac je izmenjen.");
    }

    public void DeletePublisher()
    {
        Console.WriteLine("--- Brisanje izdavaca ---");
        int id = ConsoleInput.ReadExistingId("Id izdavaca", id => _publisherDao.GetAll().Any(p => p.Id == id));
        _publisherDao.Remove(id);
        Console.WriteLine("Izdavac je obrisan.");
    }

    private Publisher ReadPublisher(int id = 0)
    {
        return new Publisher()
        {
            Id = id,
            Code = ConsoleInput.ReadUniqueText("Sifra", _publisherDao.GetAll().Where(p => p.Id != id).Select(p => p.Code)),
            Name = ConsoleInput.ReadText("Naziv"),
            HeadAuthorId = ReadHeadAuthorId(),
            AuthorIds = ConsoleInput.ReadExistingIds("Id-evi autora", id => _authorDao.GetAll().Any(a => a.Id == id)),
            BookIds = ConsoleInput.ReadExistingIds("Id-evi knjiga", id => _bookDao.GetAll().Any(b => b.Id == id))
        };
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
