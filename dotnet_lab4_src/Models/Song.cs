using AudioLibrary.Utilities;
using System;
using System.ComponentModel;

namespace AudioLibrary.Models;

public class Song : NotifyPropertyChanged, IEquatable<Song>, IEditableObject
{
    struct SongData
    {
        internal int id;
        internal string name;
        internal Author author;
        internal string albumName;
        internal Jenre jenre;
    }
    private SongData currentData;
    private SongData backupData;

    public int Id { get => currentData.id; set => currentData.id = value; }

    public string Name
    {
        get => currentData.name;
        set
        {
            currentData.name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public Author Author
    {
        get => currentData.author;
        set
        {
            BeginEdit();
            currentData.author = value;
            EndEdit();
            OnPropertyChanged(nameof(Author));
        }
    }

    public Jenre Jenre
    {
        get => currentData.jenre;
        set
        {
            BeginEdit();
            currentData.jenre = value;
            EndEdit();
            OnPropertyChanged(nameof(Jenre));
        }
    }
    public string AlbumName
    {
        get => currentData.albumName;
        set
        {
            currentData.albumName = value;
            OnPropertyChanged(nameof(AlbumName));
        }
    }


    public Song()
    {
        currentData= new SongData();
    }

    public Song(int id,string name, Author authors, Jenre jenre, string albumName) : this()
    {
        Id = id;
        Name = name;
        Author = authors;
        Jenre = jenre;
        AlbumName = albumName;
    }

    public bool Equals(Song? other)
    {
        if (other == null) return false;

        if (ReferenceEquals(this, other)) return true;

        return Name == other.Name
            && Author == other.Author;
    }

    private bool _editInProcess;
    public void BeginEdit()
    {
        if(!_editInProcess)
        {
            backupData = currentData;
            _editInProcess = true;
        }
    }

    public void CancelEdit()
    {
        if (_editInProcess)
        {
            currentData = backupData;
            _editInProcess = false;
        }
    }

    public void EndEdit()
    {
        if (_editInProcess)
        {
            backupData = new SongData();
            _editInProcess = false;
        }
    }
}
