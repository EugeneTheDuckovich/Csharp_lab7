using AudioLibrary.Entities;
using AudioLibrary.Models;

namespace AudioLibrary.Mappers;

public static class SongMapper
{
    public static Song ToModel(this SongEntity songEntity)
    {

        return new Song(songEntity.Id,songEntity.Name, 
            songEntity.Author.ToModel(), songEntity.Jenre.ToModel(), songEntity.AlbumName);
    }

    public static SongEntity ToEntity(this Song song)
    {
        return new SongEntity
        {
            Id = song.Id,
            Name = song.Name,
            JenreId = song.Jenre.Id,
            Jenre = song.Jenre.ToEntity(),
            AuthorId = song.Author.Id,
            Author = song.Author.ToEntity(),
            AlbumName = song.AlbumName
        };
    }
}