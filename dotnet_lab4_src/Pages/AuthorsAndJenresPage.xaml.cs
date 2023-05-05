using AudioLibrary.Repositories;
using AudioLibrary.Repositories.Db;
using AudioLibrary.Repositories.json;
using AudioLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AudioLibrary.Pages
{
    /// <summary>
    /// Interaction logic for AuthorsAndJenresPage.xaml
    /// </summary>
    public partial class AuthorsAndJenresPage : Page
    {
        public AuthorsAndJenresPage(Frame mainFrame)
        {
            InitializeComponent();
            DataContext = new AuthorsAndJenresViewModel(mainFrame,
                new JenreDbRepository(),new AuthorDbRepository());
        }
    }
}
