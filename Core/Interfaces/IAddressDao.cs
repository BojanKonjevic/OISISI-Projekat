using Core.Models;

namespace Core.Interfaces;

public interface IAddressDao
{
    Address Add(Address address);

    Address? Update(Address address);

    Address? Remove(int id);

    List<Address> GetAll();
}
