using AudioLibrary.Entities;
using AudioLibrary.Mappers;
using AudioLibrary.Models;
using AudioLibrary.Pages;
using AudioLibrary.Repositories.Abstract;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AudioLibrary.Repositories.json;

public class SongRepository : IRepository<Song>
{
    private const string songsPath = "save\\songs.json";
    private IEnumerable<Author> _authors;
    private IEnumerable<Jenre> _jenres;

    public SongRepository(IEnumerable<Author> authors, IEnumerable<Jenre> jenres)
    {
        _authors = authors;
        _jenres = jenres;
    }

    public IEnumerable<Song> GetAll()
    {
        if (!File.Exists(songsPath)) return new List<Song>();

        var songEntities = JsonSerializer.Deserialize<List<SongEntity>>(File.ReadAllText(songsPath));

        var songs = songEntities?.Select(s => s.ToModel()).ToList();
        songs?.RemoveAll(s => s is null);

        return songs ?? new List<Song>();
    }

    public void SaveAll(IEnumerable<Song> items)
    {
        if (!Directory.Exists("save\\")) Directory.CreateDirectory("save\\");

        var songEntities = items.Select(s => s.ToEntity()).ToList();

        File.WriteAllText(songsPath, JsonSerializer.Serialize(songEntities));
    }
}