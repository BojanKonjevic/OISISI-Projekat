using Core.Models;

namespace Core.Interfaces;

public interface IVisitorDao
{
    Visitor Add(Visitor visitor);

    List<Visitor> GetAll();
}
