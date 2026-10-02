using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GridMapper.ViewModels.Tabs;

namespace GridMapper.Views.Tabs;

public partial class FileBrowser : UserControl
{
    //  CONSTRUCTOR
    public FileBrowser()
    {
        DataContext = new FileBrowserViewModel();
        InitializeComponent();
    }
}