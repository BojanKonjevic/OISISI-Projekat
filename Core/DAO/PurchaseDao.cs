using Core.Interfaces;
using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class PurchaseDao : IPurchaseDao
{
    private static readonly string FilePath = DataStorage.PathFor("purchases.json");

    public List<Purchase> Load() => JsonFile<Purchase>.Load(FilePath);

    public void Save(List<Purchase> purchases) => JsonFile<Purchase>.Save(FilePath, purchases);
}
