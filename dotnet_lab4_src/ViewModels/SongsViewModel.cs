using AudioLibrary.Entities;
using AudioLibrary.InputModels;
using AudioLibrary.Mappers;
using AudioLibrary.Models;
using AudioLibrary.Pages;
using AudioLibrary.Repositories.Abstract;
using AudioLibrary.Utilities;
using AudioLibrary.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AudioLibrary.ViewModels;

internal class SongsViewModel : ViewModel
{
    private IRepository<Song> _songsRepository;

    private ObservableCollection<Song> _songsBackup;
    private ObservableCollection<Song> _songs;
    public ObservableCollection<Song> Songs 
    {
        get => _songs;
        set
        {
            _songs = value;
            OnPropertyChanged(nameof(_songs));
        }
    }

    public static ObservableCollection<Author> Authors { get; set; }
    public static ObservableCollection<Jenre> Jenres { get; set; }

    private SongInputModel _newSong;
    public SongInputModel NewSong
    {
        get => _newSong;
        set
        {
            _newSong = value;
            OnPropertyChanged(nameof(NewSong));
        }
    }

    public SongsViewModel(Frame frame,
        IRepository<Song> songsRepository,
        IEnumerable<Author> authors,
        IEnumerable<Jenre> jenres) : base(frame)
    {
        _songsRepository = songsRepository;

        _songsBackup = new ObservableCollection<Song>(_songsRepository.GetAll());
       Songs = new ObservableCollection<Song>(_songsBackup);
        Authors = new ObservableCollection<Author>(authors);
        Jenres = new ObservableCollection<Jenre>(jenres);

        NewSong = new SongInputModel();

        _isSorted= false;
    }

    public ICommand AddSongCommand
    {
        get => new RelayCommand(param =>
        {
            if (string.IsNullOrEmpty(NewSong.Name))
            {
                MessageBox.Show("song name must not be empty!");
                return;
            }

            if (Songs.Any(s => s.Name == NewSong.Name && s.Author.Name == NewSong.Author.Name))
            {
                MessageBox.Show("this song already exists!");
                return;
            }

            int id;
            if (Songs.Count > 0) id = Songs.MaxBy(s => s.Id).Id + 1;
            else id = 1;

            Songs.Add(new Song(id,NewSong.Name,NewSong.Author,NewSong.Jenre,NewSong.AlbumName));
            NewSong = new SongInputModel();
        });
    }

    private void ReplaceSongsList(IEnumerable<Song> songs)
    {
        Songs.Clear();
        foreach (var s in songs)
        {
            Songs.Add(s);
        }
    }

    public ICommand GoToAuthorsAndJenresPageCommand
    {
        get => new RelayCommand(param =>
        {
            Save();
            ChangeWindow(new AuthorsAndJenresPage(_mainFrame));
        });
    }
    private bool _isSorted;
    private void BackupSongsIfNotSorted()
    {
        if (_isSorted) return;

        _songsBackup = new ObservableCollection<Song>(Songs);
        _isSorted = true;
    }
    public ICommand RestoreInitialListCommand
    {
        get => new RelayCommand(param => 
        {
            if(_isSorted) ReplaceSongsList(_songsBackup);
            _isSorted= false;
        });
    }

    public ICommand SortByNameCommand
    {
        get => new RelayCommand(param =>
        {
            BackupSongsIfNotSorted();
            var sortedSongs = Songs.OrderBy(s => s.Name).ToList();
            ReplaceSongsList(sortedSongs);
        });
    }

    public ICommand SortByNameDescendingCommand
    {
        get => new RelayCommand(param =>
        {
            BackupSongsIfNotSorted();
            var sortedSongs = Songs.OrderByDescending(s => s.Name).ToList();
            ReplaceSongsList(sortedSongs);
        });
    }

    public ICommand SortByAuthorNameCommand
    {
        get => new RelayCommand(param =>
        {
            BackupSongsIfNotSorted();
            var sortedSongs = Songs.OrderBy(s => s.Author.Name).ToList();
            ReplaceSongsList(sortedSongs);
        });
    }

    public ICommand SortByAuthorNameDecsendingCommand
    {
        get => new RelayCommand(param =>
        {
            BackupSongsIfNotSorted();
            var sortedSongs = Songs.OrderByDescending(s => s.Author.Name).ToList();
            ReplaceSongsList(sortedSongs);
        });
    }

    public ICommand SortByJenreCommand
    {
        get => new RelayCommand(param =>
        {
            BackupSongsIfNotSorted();
            var sortedSongs = Songs.OrderBy(s => s.Jenre.Name).ToList();
            ReplaceSongsList(sortedSongs);
        });
    }

    public ICommand SortByJenreDecsendingCommand
    {
        get => new RelayCommand(param =>
        {
            BackupSongsIfNotSorted();
            var sortedSongs = Songs.OrderByDescending(s => s.Jenre.Name).ToList();
            ReplaceSongsList(sortedSongs);
        });
    }

    public ICommand SortByAlbumNameCommand
    {
        get => new RelayCommand(param =>
        {
            BackupSongsIfNotSorted();
            var sortedSongs = Songs.OrderBy(s => s.AlbumName).ToList();
            ReplaceSongsList(sortedSongs);
        });
    }

    public ICommand SortByAlbumNameDecsending
    {
        get => new RelayCommand(param =>
        {
            BackupSongsIfNotSorted();
            var sortedSongs = Songs.OrderByDescending(s => s.AlbumName).ToList();
            ReplaceSongsList(sortedSongs);
        });
    }

    public override void Save()
    {
        _songsRepository.SaveAll(Songs);
    }
}
