using AudioLibrary.Models;
using AudioLibrary.Repositories.Abstract;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AudioLibrary.Repositories.json;

internal class JenreRepository : IRepository<Jenre>
{
    private const string jenresPath = "save\\jenres.json";

    public IEnumerable<Jenre> GetAll()
    {
        if (!File.Exists(jenresPath)) return new List<Jenre>();

        return JsonSerializer.Deserialize<List<Jenre>>(File.ReadAllText(jenresPath)) ??
            new List<Jenre>();
    }

    public void SaveAll(IEnumerable<Jenre> items)
    {
        if (!Directory.Exists("save\\")) Directory.CreateDirectory("save\\");

        File.WriteAllText(jenresPath, JsonSerializer.Serialize(items));
    }
}
