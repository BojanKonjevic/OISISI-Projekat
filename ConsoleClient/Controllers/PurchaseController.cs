using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers;

public class PurchaseController
{
    private readonly PurchaseDao _purchaseDao;
    private readonly VisitorDao _visitorDao;
    private readonly BookDao _bookDao;

    public PurchaseController(PurchaseDao purchaseDao, VisitorDao visitorDao, BookDao bookDao)
    {
        _purchaseDao = purchaseDao;
        _visitorDao = visitorDao;
        _bookDao = bookDao;
    }

    public void AddPurchase()
    {
        Console.WriteLine("--- Dodavanje kupovine ---");
        Purchase purchase = ReadPurchase();
        _purchaseDao.Add(purchase);
        Console.WriteLine($"Kupovina je dodata sa Id {purchase.Id}.");
    }

    public void EditPurchase()
    {
        Console.WriteLine("--- Izmena kupovine ---");
        int id = ConsoleInput.ReadExistingId("Id kupovine", id => _purchaseDao.GetAll().Any(p => p.Id == id));
        Purchase purchase = ReadPurchase(id);
        _purchaseDao.Update(purchase);
        Console.WriteLine("Kupovina je izmenjena.");
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

            Console.WriteLine("Unesite broj od 1 do 5.");
        }
    }
}
