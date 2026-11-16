using Core.Interfaces;
using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class PurchaseDao : IPurchaseDao
{
    private static readonly string FilePath = DataStorage.PathFor("purchases.json");

    public List<Purchase> Load() => JsonFile<Purchase>.Load(FilePath);

    public void Save(List<Purchase> purchases) => JsonFile<Purchase>.Save(FilePath, purchases);

    public Purchase Add(Purchase purchase)
    {
        List<Purchase> purchases = Load();
        purchase.Id = purchases.Count == 0 ? 1 : purchases.Max(p => p.Id) + 1;
        purchases.Add(purchase);
        Save(purchases);
        return purchase;
    }
}
