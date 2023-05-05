using AudioLibrary.Pages;
using AudioLibrary.ViewModels.Abstract;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace AudioLibrary;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        MainFrame.Content = new SongsPage(MainFrame);

        if (!Directory.Exists("save\\")) Directory.CreateDirectory("save\\");
    }

    private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        var viewModel = (MainFrame.Content as Page)?.DataContext as ViewModel;
        
        if (viewModel is null) return;

        viewModel.Save();
    }
}
