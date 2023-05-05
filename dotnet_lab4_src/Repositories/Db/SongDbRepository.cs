using AudioLibrary.Data;
using AudioLibrary.Mappers;
using AudioLibrary.Models;
using AudioLibrary.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AudioLibrary.Repositories.Db;

public class SongDbRepository : IRepository<Song>
{
    private IEnumerable<Jenre> _jenres;
    private IEnumerable<Author> _authors;
    public SongDbRepository(IEnumerable<Jenre> jenres, IEnumerable<Author> authors)
    {
        _authors = authors;
        _jenres = jenres;
    }

    public IEnumerable<Song> GetAll()
    {
        using var context = new AudioLibraryContext();
        return context.Songs
            .Include(s => s.Author)
            .Include(s => s.Jenre)
            .AsEnumerable()
            .Select(s => 
            {
                var model = s.ToModel();
                model.Author = _authors.First(a => a.Id == model.Author.Id);
                model.Jenre = _jenres.First(a => a.Id == model.Jenre.Id);
                return model;
            })
            .ToList();
    }

    public void SaveAll(IEnumerable<Song> items)
    {
        using var context = new AudioLibraryContext();
        context.Songs.RemoveRange(context.Songs);
        foreach (var item in items)
        {
            var entity = item.ToEntity();
            entity.Author = context.Authors.First(a => a.Id == entity.Author.Id);
            entity.Jenre = context.Jenres.First(a => a.Id == entity.Jenre.Id);
            context.Songs.Add(entity);
        }
        context.SaveChanges();
    }
}