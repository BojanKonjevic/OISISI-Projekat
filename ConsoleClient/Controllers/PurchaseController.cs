using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers;

public class PurchaseController
{
    private readonly PurchaseDao _purchaseDao;

    public PurchaseController(PurchaseDao purchaseDao)
    {
        _purchaseDao = purchaseDao;
    }

    public void AddPurchase()
    {
        Console.WriteLine("--- Dodavanje kupovine ---");

        Purchase purchase = new()
        {
            VisitorId = ConsoleInput.ReadInt("Id posetioca"),
            BookId = ConsoleInput.ReadInt("Id knjige"),
            PurchaseDate = ConsoleInput.ReadDate("Datum kupovine"),
            Rating = ReadRating(),
            Comment = ConsoleInput.ReadText("Komentar")
        };

        _purchaseDao.Add(purchase);
        Console.WriteLine($"Kupovina je dodata sa Id {purchase.Id}.");
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
