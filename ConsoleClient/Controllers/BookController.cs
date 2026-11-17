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
        Book book = ReadBook();
        _bookDao.Add(book);
        Console.WriteLine($"Knjiga je dodata sa Id {book.Id}.");
    }

    public void EditBook()
    {
        Console.WriteLine("--- Izmena knjige ---");
        int id = ConsoleInput.ReadExistingId("Id knjige", id => _bookDao.GetAll().Any(b => b.Id == id));
        Book book = ReadBook(id);
        _bookDao.Update(book);
        Console.WriteLine("Knjiga je izmenjena.");
    }

    public void DeleteBook()
    {
        Console.WriteLine("--- Brisanje knjige ---");
        int id = ConsoleInput.ReadExistingId("Id knjige", id => _bookDao.GetAll().Any(b => b.Id == id));
        _bookDao.Remove(id);
        Console.WriteLine("Knjiga je obrisana.");
    }

    private Book ReadBook(int id = 0)
    {
        return new Book()
        {
            Id = id,
            Isbn = ReadIsbn(id),
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
    }

    private string ReadIsbn(int id)
    {
        while (true)
        {
            string isbn = ConsoleInput.ReadText("ISBN");
            if (!isbn.All(char.IsAsciiDigit))
            {
                Console.WriteLine("ISBN moze sadrzati samo cifre.");
                continue;
            }

            if (_bookDao.GetAll().Any(b => b.Id != id && b.Isbn == isbn))
            {
                Console.WriteLine("Vec postoji knjiga sa unetim ISBN-om.");
                continue;
            }

            return isbn;
        }
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
