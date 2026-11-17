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
        Address address = ReadAddress();
        _addressDao.Add(address);
        Console.WriteLine($"Adresa je dodata sa Id {address.Id}.");
    }

    public void EditAddress()
    {
        Console.WriteLine("--- Izmena adrese ---");
        int id = ConsoleInput.ReadExistingId("Id adrese", id => _addressDao.GetAll().Any(a => a.Id == id));
        Address address = ReadAddress(id);
        _addressDao.Update(address);
        Console.WriteLine("Adresa je izmenjena.");
    }

    private Address ReadAddress(int id = 0)
    {
        return new Address()
        {
            Id = id,
            Street = ConsoleInput.ReadText("Ulica"),
            Number = ConsoleInput.ReadText("Broj"),
            City = ConsoleInput.ReadText("Grad"),
            Country = ConsoleInput.ReadText("Drzava")
        };
    }
}
