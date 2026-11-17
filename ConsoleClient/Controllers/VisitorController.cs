using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers;

public class VisitorController
{
    private readonly VisitorDao _visitorDao;
    private readonly AddressDao _addressDao;
    private readonly BookDao _bookDao;

    public VisitorController(VisitorDao visitorDao, AddressDao addressDao, BookDao bookDao)
    {
        _visitorDao = visitorDao;
        _addressDao = addressDao;
        _bookDao = bookDao;
    }

    public void AddVisitor()
    {
        Console.WriteLine("--- Dodavanje posetioca ---");

        Visitor visitor = new()
        {
            FirstName = ConsoleInput.ReadText("Ime"),
            LastName = ConsoleInput.ReadText("Prezime"),
            DateOfBirth = ConsoleInput.ReadDate("Datum rodjenja", new DateOnly(1900, 1, 1), DateOnly.FromDateTime(DateTime.Today)),
            AddressId = ConsoleInput.ReadExistingId("Id adrese", id => _addressDao.GetAll().Any(a => a.Id == id)),
            Phone = ConsoleInput.ReadPhone("Telefon"),
            Email = ConsoleInput.ReadEmail("Email"),
            MembershipCardNumber = ConsoleInput.ReadUniqueText("Broj clanske karte", _visitorDao.GetAll().Select(v => v.MembershipCardNumber)),
            MembershipYear = ConsoleInput.ReadInt("Godina uplate clanstva", max: DateTime.Today.Year),
            Status = ReadStatus(),
            AverageRating = ReadRating(),
            PurchasedBookIds = ConsoleInput.ReadExistingIds("Id-evi kupljenih knjiga", id => _bookDao.GetAll().Any(b => b.Id == id)),
            WishlistBookIds = ConsoleInput.ReadExistingIds("Id-evi knjiga sa liste zelja", id => _bookDao.GetAll().Any(b => b.Id == id))
        };

        _visitorDao.Add(visitor);
        Console.WriteLine($"Posetilac je dodat sa Id {visitor.Id}.");
    }

    private static MembershipStatus ReadStatus()
    {
        while (true)
        {
            Console.Write("Status clanstva (1 - Regular, 2 - Vip): ");
            string input = Console.ReadLine()?.Trim() ?? string.Empty;
            if (input == "1")
            {
                return MembershipStatus.Regular;
            }

            if (input == "2")
            {
                return MembershipStatus.Vip;
            }

            Console.WriteLine("Unesite 1 ili 2.");
        }
    }

    private static double ReadRating()
    {
        while (true)
        {
            Console.Write("Prosecna ocena (0-5): ");
            if (double.TryParse(Console.ReadLine()?.Trim(), System.Globalization.CultureInfo.InvariantCulture, out double value) && value >= 0 && value <= 5)
            {
                return value;
            }

            Console.WriteLine("Unesite broj od 0 do 5.");
        }
    }
}
