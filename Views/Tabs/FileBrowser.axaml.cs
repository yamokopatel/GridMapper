using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
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
        // Initialization
        InitializeComponent();
    }

    #region  BUTTON FUNCTIONS
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
            fbvm.SetMapMetadataFilePath(files[0].Path.LocalPath);
            OnMapMetaPathChanged(files[0].Path.LocalPath);
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
            fbvm.SetMapStructureFilePath(files[0].Path.LocalPath);
            OnStructurePathChanged(files[0].Path.LocalPath);
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
            fbvm.SetCellMetadataFilePath(files[0].Path.LocalPath);
            OnCellMetaPathChanged(files[0].Path.LocalPath);
        }
    }
    #endregion

    #region EVENTS
    public static readonly RoutedEvent<StringChangedEventArgs> MapMetaPathChangedEvent =
        RoutedEvent.Register<FileBrowser, StringChangedEventArgs>(
            nameof(MapMetaPathChanged),
            RoutingStrategies.Bubble
        );
    public static readonly RoutedEvent<StringChangedEventArgs> StructurePathChangedEvent =
        RoutedEvent.Register<FileBrowser, StringChangedEventArgs>(
            nameof(StructurePathChanged),
            RoutingStrategies.Bubble
        );
    public static readonly RoutedEvent<StringChangedEventArgs> CellMetaPathChangedEvent =
        RoutedEvent.Register<FileBrowser, StringChangedEventArgs>(
            nameof(CellMetaPathChanged),
            RoutingStrategies.Bubble
        );
    #endregion

    #region EVENT HANDLERS
    public event EventHandler<StringChangedEventArgs> MapMetaPathChanged
    {
        add => AddHandler(MapMetaPathChangedEvent, value);
        remove => RemoveHandler(MapMetaPathChangedEvent, value);
    }
    public event EventHandler<StringChangedEventArgs> StructurePathChanged
    {
        add => AddHandler(StructurePathChangedEvent, value);
        remove => RemoveHandler(StructurePathChangedEvent, value);
    }
    public event EventHandler<StringChangedEventArgs> CellMetaPathChanged
    {
        add => AddHandler(CellMetaPathChangedEvent, value);
        remove => RemoveHandler(CellMetaPathChangedEvent, value);
    }
    #endregion

    #region EVENT SENDERS
    protected virtual void OnMapMetaPathChanged(string path)
    {
        RaiseEvent(new StringChangedEventArgs(MapMetaPathChangedEvent, path));
    }
    protected virtual void OnStructurePathChanged(string path)
    {
        RaiseEvent(new StringChangedEventArgs(StructurePathChangedEvent, path));
    }
    protected virtual void OnCellMetaPathChanged(string path)
    {
        RaiseEvent(new StringChangedEventArgs(CellMetaPathChangedEvent, path));
    }
    #endregion
}