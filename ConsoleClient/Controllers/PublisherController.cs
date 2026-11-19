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
        ConsoleOutput.Success($"Izdavac je dodat sa Id {publisher.Id}.");
    }

    public void EditPublisher()
    {
        Console.WriteLine("--- Izmena izdavaca ---");
        int id = ConsoleInput.ReadExistingId("Id izdavaca", id => _publisherDao.GetAll().Any(p => p.Id == id));
        Publisher publisher = ReadPublisher(id);
        _publisherDao.Update(publisher);
        ConsoleOutput.Success("Izdavac je izmenjen.");
    }

    public void ShowPublishers()
    {
        ConsoleOutput.Header("\n===== IZDAVACI =====");
        foreach (Publisher publisher in _publisherDao.GetAll())
        {
            string sefName = publisher.HeadAuthorId.HasValue
                ? (_authorDao.GetAll().FirstOrDefault(a => a.Id == publisher.HeadAuthorId.Value)?.ToString() ?? "---")
                : "-";

            Console.WriteLine($"Id: {publisher.Id} | Sifra: {publisher.Code} | Naziv: {publisher.Name} | Sef: {sefName} | Autori: [{string.Join(", ", _authorDao.GetAll().Where(a => publisher.AuthorIds.Contains(a.Id)).Select(a => a.FirstName + " " + a.LastName))}] | Knjige: [{string.Join(", ", _bookDao.GetAll().Where(b => publisher.BookIds.Contains(b.Id)).Select(b => b.Title))}]");
            ConsoleOutput.Divider();
        }
    }

    public void DeletePublisher()
    {
        Console.WriteLine("--- Brisanje izdavaca ---");
        int id = ConsoleInput.ReadExistingId("Id izdavaca", id => _publisherDao.GetAll().Any(p => p.Id == id));
        if (_bookDao.GetAll().Any(b => b.PublisherId == id))
        {
            ConsoleOutput.Error("Izdavac ne moze biti obrisan jer ga referenciraju knjige.");
            return;
        }

        _publisherDao.Remove(id);
        ConsoleOutput.Success("Izdavac je obrisan.");
    }

    private Publisher ReadPublisher(int id = 0)
    {
        return new Publisher()
        {
            Id = id,
            Code = ConsoleInput.ReadUniqueText("Sifra", _publisherDao.GetAll().Where(p => p.Id != id).Select(p => p.Code)),
            Name = ConsoleInput.ReadText("Naziv"),
            HeadAuthorId = ReadHeadAuthorId(),
            AuthorIds = ConsoleInput.ReadExistingIds("Id-evi autora", id => _authorDao.GetAll().Any(a => a.Id == id))
        };
    }

    private int? ReadHeadAuthorId()
    {
        while (true)
        {
            Console.Write("Id sefa izdavaca (prazno za preskakanje): ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;
            if (input.Length == 0)
            {
                return null;
            }

            if (!int.TryParse(input, out int id))
            {
                ConsoleOutput.Error("Id mora biti ceo broj.");
                continue;
            }

            Author? author = _authorDao.GetAll().FirstOrDefault(a => a.Id == id);
            if (author is null)
            {
                ConsoleOutput.Error($"Autor sa Id {id} ne postoji.");
                continue;
            }

            if (author.YearsOfExperience < Publisher.MinHeadAuthorExperience)
            {
                ConsoleOutput.Error($"Sef izdavaca mora imati najmanje {Publisher.MinHeadAuthorExperience} godina iskustva.");
                continue;
            }

            return id;
        }
    }
}