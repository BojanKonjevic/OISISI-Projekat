using Core.Models;

namespace Core.Interfaces;

public interface IVisitorDao
{
    List<Visitor> Load();
    void Save(List<Visitor> visitors);
}
