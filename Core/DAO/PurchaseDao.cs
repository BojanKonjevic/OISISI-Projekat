using Core.Interfaces;
using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class PurchaseDao : IPurchaseDao
{
    private readonly List<Purchase> _purchases;
    private readonly Storage<Purchase> _storage;

    public PurchaseDao()
    {
        _storage = new Storage<Purchase>("purchases.csv");
        _purchases = _storage.Load();
    }

    public Purchase Add(Purchase purchase)
    {
        purchase.Id = _purchases.Count == 0 ? 1 : _purchases.Max(p => p.Id) + 1;
        _purchases.Add(purchase);
        _storage.Save(_purchases);
        return purchase;
    }

    public List<Purchase> GetAll() => _purchases;
}
