using System.Collections.Generic;

namespace AudioLibrary.Entities;

public class AuthorEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set; }
    public List<SongEntity> Songs { get; set; }
}
