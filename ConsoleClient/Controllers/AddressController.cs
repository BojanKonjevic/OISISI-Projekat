using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers;

public class AddressController
{
    private readonly AddressDao _addressDao;

    public AddressController(AddressDao addressDao)
    {
        _addressDao = addressDao;
    }

    public void AddAddress()
    {
        Console.WriteLine("--- Dodavanje adrese ---");

        Address address = new()
        {
            Street = ConsoleInput.ReadText("Ulica"),
            Number = ConsoleInput.ReadText("Broj"),
            City = ConsoleInput.ReadText("Grad"),
            Country = ConsoleInput.ReadText("Drzava")
        };

        _addressDao.Add(address);
        Console.WriteLine($"Adresa je dodata sa Id {address.Id}.");
    }
}
