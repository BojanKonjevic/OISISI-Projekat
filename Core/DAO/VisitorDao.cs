using Core.Interfaces;
using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class VisitorDao : IVisitorDao
{
    private static readonly string FilePath = DataStorage.PathFor("visitors.json");

    public List<Visitor> Load() => JsonFile<Visitor>.Load(FilePath);

    public void Save(List<Visitor> visitors) => JsonFile<Visitor>.Save(FilePath, visitors);

    public Visitor Add(Visitor visitor)
    {
        List<Visitor> visitors = Load();
        visitor.Id = visitors.Count == 0 ? 1 : visitors.Max(v => v.Id) + 1;
        visitors.Add(visitor);
        Save(visitors);
        return visitor;
    }
}
