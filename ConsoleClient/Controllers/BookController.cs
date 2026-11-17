using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers;

public class BookController
{
    private readonly BookDao _bookDao;
    private readonly PublisherDao _publisherDao;
    private readonly AuthorDao _authorDao;
    private readonly VisitorDao _visitorDao;

    public BookController(BookDao bookDao, PublisherDao publisherDao, AuthorDao authorDao, VisitorDao visitorDao)
    {
        _bookDao = bookDao;
        _publisherDao = publisherDao;
        _authorDao = authorDao;
        _visitorDao = visitorDao;
    }

    public void AddBook()
    {
        Console.WriteLine("--- Dodavanje knjige ---");

        Book book = new()
        {
            Isbn = ConsoleInput.ReadUniqueText("ISBN", _bookDao.GetAll().Select(b => b.Isbn)),
            Title = ConsoleInput.ReadText("Naslov"),
            Genre = ConsoleInput.ReadText("Zanr"),
            PublicationYear = ConsoleInput.ReadInt("Godina izdanja", 1450, DateTime.Today.Year + 1),
            Price = ReadPrice(),
            PageCount = ConsoleInput.ReadInt("Broj strana", 1),
            AuthorIds = ConsoleInput.ReadExistingIds("Id-evi autora", id => _authorDao.GetAll().Any(a => a.Id == id)),
            PublisherId = ConsoleInput.ReadExistingId("Id izdavaca", id => _publisherDao.GetAll().Any(p => p.Id == id)),
            BuyerVisitorIds = ConsoleInput.ReadExistingIds("Id-evi posetilaca kupaca", id => _visitorDao.GetAll().Any(v => v.Id == id)),
            WishlistVisitorIds = ConsoleInput.ReadExistingIds("Id-evi posetilaca sa liste zelja", id => _visitorDao.GetAll().Any(v => v.Id == id))
        };

        _bookDao.Add(book);
        Console.WriteLine($"Knjiga je dodata sa Id {book.Id}.");
    }

    private static decimal ReadPrice()
    {
        while (true)
        {
            Console.Write("Cena: ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;
            if ((decimal.TryParse(input, System.Globalization.CultureInfo.InvariantCulture, out decimal value) ||
                 decimal.TryParse(input, out value)) && value >= 0)
            {
                return value;
            }

            Console.WriteLine("Unesite pozitivan broj za cenu.");
        }
    }
}
