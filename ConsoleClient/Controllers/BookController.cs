using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers;

public class BookController
{
    private readonly BookDao _bookDao;

    public BookController(BookDao bookDao)
    {
        _bookDao = bookDao;
    }

    public void AddBook()
    {
        Console.WriteLine("--- Dodavanje knjige ---");

        Book book = new()
        {
            Isbn = ConsoleInput.ReadText("ISBN"),
            Title = ConsoleInput.ReadText("Naslov"),
            Genre = ConsoleInput.ReadText("Zanr"),
            PublicationYear = ConsoleInput.ReadInt("Godina izdanja"),
            Price = ReadPrice(),
            PageCount = ConsoleInput.ReadInt("Broj strana"),
            AuthorIds = ConsoleInput.ReadIds("Id-evi autora"),
            PublisherId = ConsoleInput.ReadInt("Id izdavaca"),
            BuyerVisitorIds = ConsoleInput.ReadIds("Id-evi posetilaca kupaca"),
            WishlistVisitorIds = ConsoleInput.ReadIds("Id-evi posetilaca sa liste zelja")
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
