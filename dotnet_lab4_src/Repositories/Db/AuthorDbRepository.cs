using AudioLibrary.Data;
using AudioLibrary.Mappers;
using AudioLibrary.Models;
using AudioLibrary.Repositories.Abstract;
using System.Collections.Generic;
using System.Linq;

namespace AudioLibrary.Repositories.Db;

public class AuthorDbRepository : IRepository<Author>
{
    public IEnumerable<Author> GetAll()
    {
        using var context = new AudioLibraryContext();
        return context.Authors
            .AsEnumerable()
            .Select(a =>  a.ToModel())
            .ToList();
    }

    public void SaveAll(IEnumerable<Author> items)
    {
        using var context = new AudioLibraryContext();
        context.Authors.RemoveRange(context.Authors);
        foreach (var item in items)
        {
            context.Authors.Add(item.ToEntity());
        }
        context.SaveChanges();
    }
}
