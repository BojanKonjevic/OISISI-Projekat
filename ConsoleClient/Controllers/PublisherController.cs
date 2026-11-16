using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers;

public class PublisherController
{
    private readonly PublisherDao _publisherDao;

    public PublisherController(PublisherDao publisherDao)
    {
        _publisherDao = publisherDao;
    }

    public void AddPublisher()
    {
        Console.WriteLine("--- Dodavanje izdavaca ---");

        Publisher publisher = new()
        {
            Code = ConsoleInput.ReadText("Sifra"),
            Name = ConsoleInput.ReadText("Naziv"),
            HeadAuthorId = ReadHeadAuthorId(),
            AuthorIds = ConsoleInput.ReadIds("Id-evi autora"),
            BookIds = ConsoleInput.ReadIds("Id-evi knjiga")
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
