using Core.Models;

namespace Core.Interfaces;

public interface IPurchaseDao
{
    Purchase Add(Purchase purchase);

    List<Purchase> GetAll();
}
