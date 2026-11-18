using Core.DAO;
using Core.Models;

namespace ConsoleClient.Controllers;

public class AddressController
{
    private readonly AddressDao _addressDao;
    private readonly VisitorDao _visitorDao;
    private readonly AuthorDao _authorDao;

    public AddressController(AddressDao addressDao, VisitorDao visitorDao, AuthorDao authorDao)
    {
        _addressDao = addressDao;
        _visitorDao = visitorDao;
        _authorDao = authorDao;
    }

    public void AddAddress()
    {
        Console.WriteLine("--- Dodavanje adrese ---");
        Address address = ReadAddress();
        _addressDao.Add(address);
        ConsoleOutput.Success($"Adresa je dodata sa Id {address.Id}.");
    }

    public void EditAddress()
    {
        Console.WriteLine("--- Izmena adrese ---");
        int id = ConsoleInput.ReadExistingId("Id adrese", id => _addressDao.GetAll().Any(a => a.Id == id));
        Address address = ReadAddress(id);
        _addressDao.Update(address);
        ConsoleOutput.Success("Adresa je izmenjena.");
    }

    public void ShowAddresses()
    {
        ConsoleOutput.Header("\n===== ADRESE =====");
        foreach (Address address in _addressDao.GetAll())
        {
            Console.WriteLine(address);
            ConsoleOutput.Divider();
        }
    }

    public void DeleteAddress()
    {
        Console.WriteLine("--- Brisanje adrese ---");
        int id = ConsoleInput.ReadExistingId("Id adrese", id => _addressDao.GetAll().Any(a => a.Id == id));
        if (_visitorDao.GetAll().Any(v => v.AddressId == id) ||
            _authorDao.GetAll().Any(a => a.AddressId == id))
        {
            ConsoleOutput.Error("Adresa ne moze biti obrisana jer je referenciraju posetioci ili autori.");
            return;
        }

        _addressDao.Remove(id);
        ConsoleOutput.Success("Adresa je obrisana.");
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
