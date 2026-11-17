using Core.Models;

namespace Core.Interfaces;

public interface IPurchaseDao
{
    Purchase Add(Purchase purchase);

    Purchase? Update(Purchase purchase);

    Purchase? Remove(int id);

    List<Purchase> GetAll();
}
