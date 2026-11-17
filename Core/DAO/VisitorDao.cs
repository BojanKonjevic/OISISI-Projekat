using Core.Interfaces;
using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class VisitorDao : IVisitorDao
{
    private readonly List<Visitor> _visitors;
    private readonly Storage<Visitor> _storage;

    public VisitorDao()
    {
        _storage = new Storage<Visitor>("visitors.csv");
        _visitors = _storage.Load();
    }

    public Visitor Add(Visitor visitor)
    {
        visitor.Id = _visitors.Count == 0 ? 1 : _visitors.Max(v => v.Id) + 1;
        _visitors.Add(visitor);
        _storage.Save(_visitors);
        return visitor;
    }

    public Visitor? Update(Visitor visitor)
    {
        int index = _visitors.FindIndex(v => v.Id == visitor.Id);
        if (index < 0)
        {
            return null;
        }

        _visitors[index] = visitor;
        _storage.Save(_visitors);
        return visitor;
    }

    public List<Visitor> GetAll() => _visitors;
}
