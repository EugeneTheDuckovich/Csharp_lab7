using AudioLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace AudioLibrary.Data;

public class AudioLibraryContext : DbContext
{
    public DbSet<AuthorEntity> Authors { get; set; }
    public DbSet<JenreEntity> Jenres { get; set; }
    public DbSet<SongEntity> Songs { get; set; }

    public AudioLibraryContext()
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseSqlite("Data Source=AudioLibrary.db");
    }
}
