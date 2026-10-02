using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GridMapper.ViewModels.Tabs;

public partial class FileBrowserViewModel : ViewModelBase
{
    //  PROPERTIES
    [ObservableProperty]
    public partial string MapMetadataFilePath { get; set; }
    [ObservableProperty]
    public partial string MapStructureFilePath { get; set; }
    [ObservableProperty]
    public partial string CellMetadataFilePath { get; set; }

    public event Action<string[]>? PathsProvided;
    protected virtual void OnPathsProvided(string[] paths)
    {
        PathsProvided?.Invoke(paths);
    }

    public void SendPaths()
    {
        if(!string.IsNullOrWhiteSpace(MapMetadataFilePath)
            && !string.IsNullOrWhiteSpace(MapStructureFilePath)
            && !string.IsNullOrWhiteSpace(CellMetadataFilePath))
        {
            OnPathsProvided([MapMetadataFilePath, MapStructureFilePath, CellMetadataFilePath]);
        }
    }
}