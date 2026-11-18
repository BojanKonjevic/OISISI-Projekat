using Core.DAO;
using Core.Models;
using Core.Services;

namespace ConsoleClient.Controllers;

public class PurchaseController
{
    private readonly PurchaseDao _purchaseDao;
    private readonly VisitorDao _visitorDao;
    private readonly BookDao _bookDao;
    private readonly ReferenceSyncService _referenceSync;

    public PurchaseController(PurchaseDao purchaseDao, VisitorDao visitorDao, BookDao bookDao, ReferenceSyncService referenceSync)
    {
        _purchaseDao = purchaseDao;
        _visitorDao = visitorDao;
        _bookDao = bookDao;
        _referenceSync = referenceSync;
    }

    public void AddPurchase()
    {
        Console.WriteLine("--- Dodavanje kupovine ---");
        Purchase purchase = ReadPurchase();
        _purchaseDao.Add(purchase);
        _referenceSync.SyncAll();
        ConsoleOutput.Success($"Kupovina je dodata sa Id {purchase.Id}.");
    }

    public void EditPurchase()
    {
        Console.WriteLine("--- Izmena kupovine ---");
        int id = ConsoleInput.ReadExistingId("Id kupovine", id => _purchaseDao.GetAll().Any(p => p.Id == id));
        Purchase purchase = ReadPurchase(id);
        _purchaseDao.Update(purchase);
        _referenceSync.SyncAll();
        ConsoleOutput.Success("Kupovina je izmenjena.");
    }

    public void ShowPurchases()
    {
        ConsoleOutput.Header("\n===== KUPOVINE =====");
        foreach (Purchase purchase in _purchaseDao.GetAll())
        {
            Console.WriteLine(purchase);
            ConsoleOutput.Divider();
        }
    }

    public void DeletePurchase()
    {
        Console.WriteLine("--- Brisanje kupovine ---");
        int id = ConsoleInput.ReadExistingId("Id kupovine", id => _purchaseDao.GetAll().Any(p => p.Id == id));
        _purchaseDao.Remove(id);
        _referenceSync.SyncAll();
        ConsoleOutput.Success("Kupovina je obrisana.");
    }

    private Purchase ReadPurchase(int id = 0)
    {
        return new Purchase()
        {
            Id = id,
            VisitorId = ConsoleInput.ReadExistingId("Id posetioca", id => _visitorDao.GetAll().Any(v => v.Id == id)),
            BookId = ConsoleInput.ReadExistingId("Id knjige", id => _bookDao.GetAll().Any(b => b.Id == id)),
            PurchaseDate = ConsoleInput.ReadDate("Datum kupovine", max: DateOnly.FromDateTime(DateTime.Today)),
            Rating = ReadRating(),
            Comment = ConsoleInput.ReadText("Komentar")
        };
    }

    private static int ReadRating()
    {
        while (true)
        {
            Console.Write("Ocena (1-5): ");
            if (int.TryParse(Console.ReadLine()?.Trim(), out int value) && value >= 1 && value <= 5)
            {
                return value;
            }

            ConsoleOutput.Error("Unesite broj od 1 do 5.");
        }
    }
}
