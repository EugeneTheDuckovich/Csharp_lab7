using AudioLibrary.InputModels;
using AudioLibrary.Models;
using AudioLibrary.Pages;
using AudioLibrary.Repositories;
using AudioLibrary.Repositories.Abstract;
using AudioLibrary.Utilities;
using AudioLibrary.ViewModels.Abstract;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace AudioLibrary.ViewModels;

public class AuthorsAndJenresViewModel : ViewModel
{
    private IRepository<Jenre> _jenresRepository;
    private IRepository<Author> _authorsRepository;

    public ObservableCollection<Jenre> Jenres { get; }
    public ObservableCollection<Author> Authors { get; }

    private AuthorInputModel _newAuthor;
    public AuthorInputModel NewAuthor 
    {
        get => _newAuthor;
        set
        {
            if (_newAuthor == value) return;
            _newAuthor = value;
            OnPropertyChanged(nameof(NewAuthor));
        }
    }

    private JenreInputModel _newJenre;
    public JenreInputModel NewJenre
    {
        get => _newJenre;
        set
        {
            if (_newJenre == value) return;
            _newJenre = value;
            OnPropertyChanged(nameof(NewJenre));
        }
    }

    public AuthorsAndJenresViewModel(Frame frame, 
        IRepository<Jenre> jenresRepository, IRepository<Author> authorRepository) : base(frame)
    {
        _jenresRepository = jenresRepository;
        _authorsRepository = authorRepository;

        Jenres = new ObservableCollection<Jenre>(_jenresRepository.GetAll());
        Authors = new ObservableCollection<Author>(_authorsRepository.GetAll());

        NewJenre = new JenreInputModel();
        NewAuthor = new AuthorInputModel();
    }


    private ICommand _addJenreCommand;
    public ICommand AddJenreCommand
    {
        get => _addJenreCommand ?? new RelayCommand(param =>
        {
            if(string.IsNullOrWhiteSpace(NewJenre.Name))
            {
                MessageBox.Show("Jenre must not be empty!");
                return;
            }

            if (Jenres.Any(j => j.Name == NewJenre.Name))
            {
                MessageBox.Show("this jenre already exists!");
                return;
            }            

            int id;

            if (Jenres.Count == 0) id = 1;
            else id = Jenres.MaxBy(j => j.Id).Id + 1;

            Jenres.Add(new Jenre(id, NewJenre.Name));
            NewJenre = new JenreInputModel();
        });
    }


    private ICommand addAuthorCommand;
    public ICommand AddAuthorCommand
    {
        get => addAuthorCommand ?? new RelayCommand(param =>
        {
            if (string.IsNullOrWhiteSpace(NewAuthor.Name))
            {
                MessageBox.Show("name cannot be empty!");
                return;
            }

            int year;
            if(!int.TryParse(NewAuthor.Year, out year))
            {
                MessageBox.Show("year must be a number!");
                return;
            }

            if (Authors.Any(a => a.Name == NewAuthor.Name))
            {
                MessageBox.Show("this author already exists!");
                return;
            }

            int id;
            if (Authors.Count == 0) id = 1;
            else id = Authors.MaxBy(a => a.Id).Id + 1;

            try
            {
                Authors.Add(new Author(id, NewAuthor.Name, year));
                NewAuthor = new AuthorInputModel();
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("year out of range!");
            }
        });
    }


    private ICommand _goToSongsPageCommand;
    public ICommand GoToSongsPageCommand
    {
        get => _goToSongsPageCommand ?? new RelayCommand(param =>
        {
            Save();
            ChangeWindow(new SongsPage(_mainFrame));
        });
    }

    public override void Save()
    {
        _jenresRepository.SaveAll(Jenres);
        _authorsRepository.SaveAll(Authors);
    }
}