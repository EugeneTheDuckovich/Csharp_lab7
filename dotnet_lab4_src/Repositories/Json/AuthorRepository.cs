using AudioLibrary.Models;
using AudioLibrary.Repositories.Abstract;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AudioLibrary.Repositories.json;

internal class AuthorRepository : IRepository<Author>
{
    private const string authorsPath = "save\\authors.json";

    public IEnumerable<Author> GetAll()
    {
        if (!File.Exists(authorsPath)) return new List<Author>();
        return JsonSerializer.Deserialize<List<Author>>(File.ReadAllText(authorsPath)) ??
            new List<Author>();
    }

    public void SaveAll(IEnumerable<Author> items)
    {
        if (!Directory.Exists("save\\")) Directory.CreateDirectory("save\\");

        File.WriteAllText(authorsPath, JsonSerializer.Serialize(items));
    }
}
