using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using GridMapper.ViewModels.Tabs;

namespace GridMapper.Views.Tabs;

public partial class FileBrowser : UserControl
{
    public static readonly StyledProperty<DataBearer> BearerInProperty =
        AvaloniaProperty.Register<FileBrowser, DataBearer>(nameof(BearerIn));
    public DataBearer BearerIn
    {
        get => GetValue(BearerInProperty);
        set => SetValue(BearerInProperty, value);
    }
    private FileBrowserViewModel fbvm;
    //  CONSTRUCTOR
    public FileBrowser()
    {
        fbvm = new FileBrowserViewModel();
        DataContext = fbvm;
        fbvm.BearerIn = BearerIn;
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
    //  Select Map Struct
    private async void SelectMapStruct_Clicked(object sender, RoutedEventArgs args)
    {
        TopLevel topLevel = TopLevel.GetTopLevel(this)!;

        IReadOnlyList<IStorageFile> files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select Map Structure Data File",
            AllowMultiple = false,
            FileTypeFilter = new[] { new FilePickerFileType("BIN files"){Patterns = new[]{"*.bin", "*.bytes"}}},
        });
        if(files.Count >= 1)
        {
            fbvm.MapStructureFilePath = files[0].Path.LocalPath;
        }
    }
    //  Select Cell Meta
    private async void SelectCellMeta_Clicked(object sender, RoutedEventArgs args)
    {
        TopLevel topLevel = TopLevel.GetTopLevel(this)!;

        IReadOnlyList<IStorageFile> files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select Cell Metadata File",
            AllowMultiple = false,
            FileTypeFilter = new[] {FilePickerFileTypes.Json},
        });
        if(files.Count >= 1)
        {
            fbvm.CellMetadataFilePath = files[0].Path.LocalPath;
        }
    }
}