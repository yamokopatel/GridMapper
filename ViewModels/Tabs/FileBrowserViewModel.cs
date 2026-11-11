using CommunityToolkit.Mvvm.ComponentModel;

namespace GridMapper.ViewModels.Tabs;

public partial class FileBrowserViewModel : ViewModelBase
{
    //  PROPERTIES
    [ObservableProperty]
    public partial string MapMetadataFilePath { get; private set; } = "";
    [ObservableProperty]
    public partial string MapStructureFilePath { get; private set; } = "";
    [ObservableProperty]
    public partial string CellMetadataFilePath { get; private set; } = "";
    // [ObservableProperty]
    // public partial string DebugText { get; set; } = "";

    public FileBrowserViewModel(){}

    public void SetMapMetadataFilePath(string path)
    {
        MapMetadataFilePath = path;
    }
    public void SetMapStructureFilePath(string path)
    {
        MapStructureFilePath = path;
    }
    public void SetCellMetadataFilePath(string path)
    {
        CellMetadataFilePath = path;
    }
}