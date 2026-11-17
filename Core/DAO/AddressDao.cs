using Core.Interfaces;
using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class AddressDao : IAddressDao
{
    private readonly List<Address> _addresses;
    private readonly Storage<Address> _storage;

    public AddressDao()
    {
        _storage = new Storage<Address>("addresses.csv");
        _addresses = _storage.Load();
    }

    public Address Add(Address address)
    {
        address.Id = _addresses.Count == 0 ? 1 : _addresses.Max(a => a.Id) + 1;
        _addresses.Add(address);
        _storage.Save(_addresses);
        return address;
    }

    public List<Address> GetAll() => _addresses;
}
