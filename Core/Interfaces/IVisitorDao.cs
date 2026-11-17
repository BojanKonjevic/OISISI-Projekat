using Core.Models;

namespace Core.Interfaces;

public interface IVisitorDao
{
    Visitor Add(Visitor visitor);

    Visitor? Update(Visitor visitor);

    List<Visitor> GetAll();
}
