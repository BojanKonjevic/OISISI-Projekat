using Core.DAO;
using Core.Models;
using Core.Services;

namespace ConsoleClient.Controllers;

public class VisitorController
{
    private readonly VisitorDao _visitorDao;
    private readonly AddressDao _addressDao;
    private readonly BookDao _bookDao;
    private readonly PurchaseDao _purchaseDao;
    private readonly ReferenceSyncService _referenceSync;

    public VisitorController(VisitorDao visitorDao, AddressDao addressDao, BookDao bookDao, PurchaseDao purchaseDao, ReferenceSyncService referenceSync)
    {
        _visitorDao = visitorDao;
        _addressDao = addressDao;
        _bookDao = bookDao;
        _purchaseDao = purchaseDao;
        _referenceSync = referenceSync;
    }

    public void AddVisitor()
    {
        Console.WriteLine("--- Dodavanje posetioca ---");
        Visitor visitor = ReadVisitor();
        _visitorDao.Add(visitor);
        _referenceSync.SyncAll();
        ConsoleOutput.Success($"Posetilac je dodat sa Id {visitor.Id}.");
    }

    public void EditVisitor()
    {
        Console.WriteLine("--- Izmena posetioca ---");
        int id = ConsoleInput.ReadExistingId("Id posetioca", id => _visitorDao.GetAll().Any(v => v.Id == id));
        Visitor visitor = ReadVisitor(id);
        _visitorDao.Update(visitor);
        _referenceSync.SyncAll();
        ConsoleOutput.Success("Posetilac je izmenjen.");
    }

    public void ShowVisitors()
    {
        ConsoleOutput.Header("\n===== POSETIOCI =====");
        foreach (Visitor visitor in _visitorDao.GetAll())
        {
            Console.WriteLine(visitor);
            ConsoleOutput.Divider();
        }
    }

    public void DeleteVisitor()
    {
        Console.WriteLine("--- Brisanje posetioca ---");
        int id = ConsoleInput.ReadExistingId("Id posetioca", id => _visitorDao.GetAll().Any(v => v.Id == id));
        if (_purchaseDao.GetAll().Any(p => p.VisitorId == id))
        {
            ConsoleOutput.Error("Posetilac ne moze biti obrisan jer ima kupovine.");
            return;
        }

        _visitorDao.Remove(id);
        _referenceSync.SyncAll();
        ConsoleOutput.Success("Posetilac je obrisan.");
    }

    private Visitor ReadVisitor(int id = 0)
    {
        return new Visitor()
        {
            Id = id,
            FirstName = ConsoleInput.ReadText("Ime"),
            LastName = ConsoleInput.ReadText("Prezime"),
            DateOfBirth = ConsoleInput.ReadDate("Datum rodjenja", new DateOnly(1900, 1, 1), DateOnly.FromDateTime(DateTime.Today)),
            AddressId = ConsoleInput.ReadExistingId("Id adrese", id => _addressDao.GetAll().Any(a => a.Id == id)),
            Phone = ConsoleInput.ReadPhone("Telefon"),
            Email = ConsoleInput.ReadEmail("Email"),
            MembershipCardNumber = ConsoleInput.ReadUniqueText("Broj clanske karte", _visitorDao.GetAll().Where(v => v.Id != id).Select(v => v.MembershipCardNumber)),
            MembershipYear = ConsoleInput.ReadInt("Godina uplate clanstva", max: DateTime.Today.Year),
            Status = ReadStatus(),
            AverageRating = ReadRating(),
            WishlistBookIds = ConsoleInput.ReadExistingIds("Id-evi knjiga sa liste zelja", id => _bookDao.GetAll().Any(b => b.Id == id))
        };
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

            ConsoleOutput.Error("Unesite 1 ili 2.");
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

            ConsoleOutput.Error("Unesite broj od 0 do 5.");
        }
    }
}
