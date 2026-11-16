using ConsoleClient.Controllers;
using Core.DAO;

VisitorDao visitorDao = new();
PurchaseDao purchaseDao = new();
PublisherDao publisherDao = new();

VisitorController visitorController = new(visitorDao);
PurchaseController purchaseController = new(purchaseDao);
PublisherController publisherController = new(publisherDao);

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== Sajam knjiga ===");
    Console.WriteLine("1. Dodaj posetioca");
    Console.WriteLine("2. Dodaj kupovinu");
    Console.WriteLine("3. Dodaj izdavaca");
    Console.WriteLine("0. Izlaz");
    Console.Write("Izbor: ");

    string? choice = Console.ReadLine()?.Trim();

    if (choice is null || choice == "0")
    {
        break;
    }

    switch (choice)
    {
        case "1":
            visitorController.AddVisitor();
            break;
        case "2":
            purchaseController.AddPurchase();
            break;
        case "3":
            publisherController.AddPublisher();
            break;
        default:
            Console.WriteLine("Nepostojeca opcija.");
            break;
    }
}
