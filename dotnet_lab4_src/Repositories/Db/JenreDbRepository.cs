using AudioLibrary.Data;
using AudioLibrary.Mappers;
using AudioLibrary.Models;
using AudioLibrary.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace AudioLibrary.Repositories.Db;

public class JenreDbRepository : IRepository<Jenre>
{
    public IEnumerable<Jenre> GetAll()
    {
        using var context = new AudioLibraryContext();
        return context.Jenres
            .AsEnumerable()
            .Select(j => j.ToModel())
            .ToList();
    }

    public void SaveAll(IEnumerable<Jenre> items)
    {
        using var context = new AudioLibraryContext();
        context.Jenres.RemoveRange(context.Jenres);
        foreach (var item in items)
        {
            context.Jenres.Add(item.ToEntity());
        }
        context.SaveChanges();
    }
}
