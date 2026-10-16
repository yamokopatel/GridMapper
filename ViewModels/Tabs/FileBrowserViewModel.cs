using CommunityToolkit.Mvvm.ComponentModel;

namespace GridMapper.ViewModels.Tabs;

public partial class FileBrowserViewModel : ViewModelBase
{
    public DataBearer BearerIn;
    //  PROPERTIES
    [ObservableProperty]
    public partial string MapMetadataFilePath { get; private set; } = "";
    [ObservableProperty]
    public partial string MapStructureFilePath { get; private set; } = "";
    [ObservableProperty]
    public partial string CellMetadataFilePath { get; private set; } = "";
    // [ObservableProperty]
    // public partial string DebugText { get; set; } = "";

    public FileBrowserViewModel(DataBearer bearerIn)
    {
        BearerIn = bearerIn;
        // DebugText = (BearerIn == null).ToString();
    }

    public void SetMapMetadataFilePath(string path)
    {
        MapMetadataFilePath = path;
        BearerIn!.SetMapMetaPath(path);
    }
    public void SetMapStructureFilePath(string path)
    {
        MapStructureFilePath = path;
        BearerIn!.SetStructurePath(path);
    }
    public void SetCellMetadataFilePath(string path)
    {
        CellMetadataFilePath = path;
        BearerIn!.SetCellMetaPath(path);
    }
}