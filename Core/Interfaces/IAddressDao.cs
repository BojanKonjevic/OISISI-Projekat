using Core.Models;

namespace Core.Interfaces;

public interface IAddressDao
{
    Address Add(Address address);

    Address? Update(Address address);

    List<Address> GetAll();
}
