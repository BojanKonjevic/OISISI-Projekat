using ConsoleClient.Controllers;
using Core.DAO;

VisitorDao visitorDao = new();
PurchaseDao purchaseDao = new();
PublisherDao publisherDao = new();
AddressDao addressDao = new();
AuthorDao authorDao = new();
BookDao bookDao = new();

VisitorController visitorController = new(visitorDao);
PurchaseController purchaseController = new(purchaseDao);
PublisherController publisherController = new(publisherDao);
AddressController addressController = new(addressDao);
AuthorController authorController = new(authorDao);
BookController bookController = new(bookDao);

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== Sajam knjiga ===");
    Console.WriteLine("1. Dodaj posetioca");
    Console.WriteLine("2. Dodaj kupovinu");
    Console.WriteLine("3. Dodaj izdavaca");
    Console.WriteLine("4. Dodaj adresu");
    Console.WriteLine("5. Dodaj autora");
    Console.WriteLine("6. Dodaj knjigu");
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
        case "4":
            addressController.AddAddress();
            break;
        case "5":
            authorController.AddAuthor();
            break;
        case "6":
            bookController.AddBook();
            break;
        default:
            Console.WriteLine("Nepostojeca opcija.");
            break;
    }
}
