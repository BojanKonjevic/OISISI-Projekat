using Core.Interfaces;
using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class AddressDao : IAddressDao
{
    private static readonly string FilePath = DataStorage.PathFor("addresses.json");

    public List<Address> Load() => JsonFile<Address>.Load(FilePath);

    public void Save(List<Address> addresses) => JsonFile<Address>.Save(FilePath, addresses);
}
