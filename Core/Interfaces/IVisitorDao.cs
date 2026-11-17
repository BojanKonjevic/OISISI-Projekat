using Core.Models;

namespace Core.Interfaces;

public interface IVisitorDao
{
    Visitor Add(Visitor visitor);

    Visitor? Update(Visitor visitor);

    Visitor? Remove(int id);

    List<Visitor> GetAll();
}
