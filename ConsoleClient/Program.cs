using ConsoleClient.Controllers;
using Core.DAO;
using Core.Services;

VisitorDao visitorDao = new();
PurchaseDao purchaseDao = new();
PublisherDao publisherDao = new();
AddressDao addressDao = new();
AuthorDao authorDao = new();
BookDao bookDao = new();

ReferenceSyncService referenceSync = new(visitorDao, purchaseDao, publisherDao, authorDao, bookDao);
referenceSync.SyncAll();

VisitorController visitorController = new(visitorDao, addressDao, bookDao, purchaseDao, referenceSync);
PurchaseController purchaseController = new(purchaseDao, visitorDao, bookDao, referenceSync);
PublisherController publisherController = new(publisherDao, authorDao, bookDao);
AddressController addressController = new(addressDao, visitorDao, authorDao);
AuthorController authorController = new(authorDao, addressDao, bookDao, publisherDao, referenceSync);
BookController bookController = new(bookDao, publisherDao, authorDao, purchaseDao, referenceSync);

while (true)
{
    Console.WriteLine();
    ConsoleOutput.Header("=== Sajam knjiga ===");
    Console.WriteLine("1. Posetioci");
    Console.WriteLine("2. Kupovine");
    Console.WriteLine("3. Izdavaci");
    Console.WriteLine("4. Adrese");
    Console.WriteLine("5. Autori");
    Console.WriteLine("6. Knjige");
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
            RunEntityMenu("Posetioci", visitorController.AddVisitor, visitorController.EditVisitor, visitorController.DeleteVisitor, visitorController.ShowVisitors);
            break;
        case "2":
            RunEntityMenu("Kupovine", purchaseController.AddPurchase, purchaseController.EditPurchase, purchaseController.DeletePurchase, purchaseController.ShowPurchases);
            break;
        case "3":
            RunEntityMenu("Izdavaci", publisherController.AddPublisher, publisherController.EditPublisher, publisherController.DeletePublisher, publisherController.ShowPublishers);
            break;
        case "4":
            RunEntityMenu("Adrese", addressController.AddAddress, addressController.EditAddress, addressController.DeleteAddress, addressController.ShowAddresses);
            break;
        case "5":
            RunEntityMenu("Autori", authorController.AddAuthor, authorController.EditAuthor, authorController.DeleteAuthor, authorController.ShowAuthors);
            break;
        case "6":
            RunEntityMenu("Knjige", bookController.AddBook, bookController.EditBook, bookController.DeleteBook, bookController.ShowBooks);
            break;
        default:
            ConsoleOutput.Error("Nepostojeca opcija.");
            break;
    }
}

void RunEntityMenu(string title, Action add, Action edit, Action remove, Action show)
{
    while (true)
    {
        Console.WriteLine();
        ConsoleOutput.Header($"=== {title} ===");
        Console.WriteLine("1. Dodaj");
        Console.WriteLine("2. Izmeni");
        Console.WriteLine("3. Obrisi");
        Console.WriteLine("4. Prikazi");
        Console.WriteLine("0. Nazad");
        Console.Write("Izbor: ");

        string? choice = Console.ReadLine()?.Trim();

        if (choice is null || choice == "0")
        {
            return;
        }

        switch (choice)
        {
            case "1":
                add();
                break;
            case "2":
                edit();
                break;
            case "3":
                remove();
                break;
            case "4":
                show();
                break;
            default:
                ConsoleOutput.Error("Nepostojeca opcija.");
                break;
        }
    }
}
