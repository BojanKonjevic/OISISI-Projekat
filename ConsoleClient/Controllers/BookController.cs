using Core.DAO;
using Core.Models;
using Core.Services;

namespace ConsoleClient.Controllers;

public class BookController
{
    private readonly BookDao _bookDao;
    private readonly PublisherDao _publisherDao;
    private readonly AuthorDao _authorDao;
    private readonly PurchaseDao _purchaseDao;
    private readonly ReferenceSyncService _referenceSync;

    public BookController(BookDao bookDao, PublisherDao publisherDao, AuthorDao authorDao, PurchaseDao purchaseDao, ReferenceSyncService referenceSync)
    {
        _bookDao = bookDao;
        _publisherDao = publisherDao;
        _authorDao = authorDao;
        _purchaseDao = purchaseDao;
        _referenceSync = referenceSync;
    }

    public void AddBook()
    {
        Console.WriteLine("--- Dodavanje knjige ---");
        Book book = ReadBook();
        _bookDao.Add(book);
        _referenceSync.SyncAll();
        ConsoleOutput.Success($"Knjiga je dodata sa Id {book.Id}.");
    }

    public void EditBook()
    {
        Console.WriteLine("--- Izmena knjige ---");
        int id = ConsoleInput.ReadExistingId("Id knjige", id => _bookDao.GetAll().Any(b => b.Id == id));
        Book book = ReadBook(id);
        _bookDao.Update(book);
        _referenceSync.SyncAll();
        ConsoleOutput.Success("Knjiga je izmenjena.");
    }

    public void ShowBooks()
    {
        ConsoleOutput.Header("\n===== KNJIGE =====");
        foreach (Book book in _bookDao.GetAll())
        {
            Console.WriteLine(book);
            ConsoleOutput.Divider();
        }
    }

    public void DeleteBook()
    {
        Console.WriteLine("--- Brisanje knjige ---");
        int id = ConsoleInput.ReadExistingId("Id knjige", id => _bookDao.GetAll().Any(b => b.Id == id));
        if (_purchaseDao.GetAll().Any(p => p.BookId == id))
        {
            ConsoleOutput.Error("Knjiga ne moze biti obrisana jer postoje kupovine te knjige.");
            return;
        }

        _bookDao.Remove(id);
        _referenceSync.SyncAll();
        ConsoleOutput.Success("Knjiga je obrisana.");
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
        };
    }

    private string ReadIsbn(int id)
    {
        while (true)
        {
            string isbn = ConsoleInput.ReadText("ISBN");
            if (!isbn.All(char.IsAsciiDigit))
            {
                ConsoleOutput.Error("ISBN moze sadrzati samo cifre.");
                continue;
            }

            if (_bookDao.GetAll().Any(b => b.Id != id && b.Isbn == isbn))
            {
                ConsoleOutput.Error("Vec postoji knjiga sa unetim ISBN-om.");
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

            ConsoleOutput.Error("Unesite pozitivan broj za cenu.");
        }
    }
}
