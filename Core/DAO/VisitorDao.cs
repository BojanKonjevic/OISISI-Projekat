using Core.Interfaces;
using Core.Models;
using Core.Utils;

namespace Core.DAO;

public class VisitorDao : IVisitorDao
{
    private static readonly string FilePath = DataStorage.PathFor("visitors.json");

    public List<Visitor> Load() => JsonFile<Visitor>.Load(FilePath);

    public void Save(List<Visitor> visitors) => JsonFile<Visitor>.Save(FilePath, visitors);
}
