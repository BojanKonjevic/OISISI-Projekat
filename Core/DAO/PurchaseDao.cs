using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class PurchaseDao
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

    public Purchase? Update(Purchase purchase)
    {
        int index = _purchases.FindIndex(p => p.Id == purchase.Id);
        if (index < 0)
        {
            return null;
        }

        _purchases[index] = purchase;
        _storage.Save(_purchases);
        return purchase;
    }

    public Purchase? Remove(int id)
    {
        Purchase? purchase = _purchases.Find(p => p.Id == id);
        if (purchase is null)
        {
            return null;
        }

        _purchases.Remove(purchase);
        _storage.Save(_purchases);
        return purchase;
    }

    public List<Purchase> GetAll() => _purchases;
}
