using System.Collections.Generic;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using GridMapper.ViewModels.Tabs;

namespace GridMapper.Views.Tabs;

public partial class FileBrowser : UserControl
{
    private FileBrowserViewModel fbvm;
    //  CONSTRUCTOR
    public FileBrowser()
    {
        fbvm = new FileBrowserViewModel();
        DataContext = fbvm;
        InitializeComponent();
    }

    //  BUTTON FUNCTIONS
    //  Select Map Meta
    private async void SelectMapMeta_Clicked(object sender, RoutedEventArgs args)
    {
        TopLevel topLevel = TopLevel.GetTopLevel(this)!;

        IReadOnlyList<IStorageFile> files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select Map Metadata File",
            AllowMultiple = false,
            FileTypeFilter = new[] {FilePickerFileTypes.Json},
        });
        if(files.Count >= 1)
        {
            fbvm.MapMetadataFilePath = files[0].Path.LocalPath;
        }
    }
}