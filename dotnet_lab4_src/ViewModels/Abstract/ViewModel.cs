using AudioLibrary.Utilities;
using System.Windows.Controls;

namespace AudioLibrary.ViewModels.Abstract;

public abstract class ViewModel : NotifyPropertyChanged
{
    protected Frame _mainFrame;

    public ViewModel(Frame frame)
    {
        _mainFrame = frame;
    }

    protected void ChangeWindow(Page page)
    {
        _mainFrame.Content = page;
    }

    public abstract void Save();
}
