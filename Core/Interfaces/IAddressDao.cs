using Core.Models;

namespace Core.Interfaces;

public interface IAddressDao
{
    List<Address> Load();
    void Save(List<Address> addresses);
}
