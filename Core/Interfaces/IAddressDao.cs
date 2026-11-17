using Core.Models;

namespace Core.Interfaces;

public interface IAddressDao
{
    Address Add(Address address);

    List<Address> GetAll();
}
