using ConsoleClient.Controllers;
using Core.DAO;

VisitorDao visitorDao = new();
PurchaseDao purchaseDao = new();
PublisherDao publisherDao = new();
AddressDao addressDao = new();
AuthorDao authorDao = new();
BookDao bookDao = new();

VisitorController visitorController = new(visitorDao, addressDao, bookDao);
PurchaseController purchaseController = new(purchaseDao, visitorDao, bookDao);
PublisherController publisherController = new(publisherDao, authorDao, bookDao);
AddressController addressController = new(addressDao);
AuthorController authorController = new(authorDao, addressDao, bookDao);
BookController bookController = new(bookDao, publisherDao, authorDao, visitorDao);

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
    Console.WriteLine("7. Izmeni posetioca");
    Console.WriteLine("8. Izmeni kupovinu");
    Console.WriteLine("9. Izmeni izdavaca");
    Console.WriteLine("10. Izmeni adresu");
    Console.WriteLine("11. Izmeni autora");
    Console.WriteLine("12. Izmeni knjigu");
    Console.WriteLine("13. Obrisi posetioca");
    Console.WriteLine("14. Obrisi kupovinu");
    Console.WriteLine("15. Obrisi izdavaca");
    Console.WriteLine("16. Obrisi adresu");
    Console.WriteLine("17. Obrisi autora");
    Console.WriteLine("18. Obrisi knjigu");
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
        case "7":
            visitorController.EditVisitor();
            break;
        case "8":
            purchaseController.EditPurchase();
            break;
        case "9":
            publisherController.EditPublisher();
            break;
        case "10":
            addressController.EditAddress();
            break;
        case "11":
            authorController.EditAuthor();
            break;
        case "12":
            bookController.EditBook();
            break;
        case "13":
            visitorController.DeleteVisitor();
            break;
        case "14":
            purchaseController.DeletePurchase();
            break;
        case "15":
            publisherController.DeletePublisher();
            break;
        case "16":
            addressController.DeleteAddress();
            break;
        case "17":
            authorController.DeleteAuthor();
            break;
        case "18":
            bookController.DeleteBook();
            break;
        default:
            Console.WriteLine("Nepostojeca opcija.");
            break;
    }
}
