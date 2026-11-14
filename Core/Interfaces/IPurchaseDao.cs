using Core.Models;

namespace Core.Interfaces;

public interface IPurchaseDao
{
    List<Purchase> Load();
    void Save(List<Purchase> purchases);
}
