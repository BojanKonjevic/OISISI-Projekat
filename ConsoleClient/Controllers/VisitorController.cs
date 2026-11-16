using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers;

public class VisitorController
{
    private readonly VisitorDao _visitorDao;

    public VisitorController(VisitorDao visitorDao)
    {
        _visitorDao = visitorDao;
    }

    public void AddVisitor()
    {
        Console.WriteLine("--- Dodavanje posetioca ---");

        Visitor visitor = new()
        {
            FirstName = ConsoleInput.ReadText("Ime"),
            LastName = ConsoleInput.ReadText("Prezime"),
            DateOfBirth = ConsoleInput.ReadDate("Datum rodjenja"),
            AddressId = ConsoleInput.ReadInt("Id adrese"),
            Phone = ConsoleInput.ReadText("Telefon"),
            Email = ConsoleInput.ReadText("Email"),
            MembershipCardNumber = ConsoleInput.ReadText("Broj clanske karte"),
            MembershipYear = ConsoleInput.ReadInt("Godina uplate clanstva"),
            Status = ReadStatus(),
            AverageRating = ReadRating(),
            PurchasedBookIds = ConsoleInput.ReadIds("Id-evi kupljenih knjiga"),
            WishlistBookIds = ConsoleInput.ReadIds("Id-evi knjiga sa liste zelja")
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
