using CommunityToolkit.Mvvm.ComponentModel;

namespace GridMapper.ViewModels.Tabs;

public partial class FileBrowserViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial DataBearer BearerIn { get; set; }
    //  PROPERTIES
    [ObservableProperty]
    public partial string MapMetadataFilePath { get; set; }
    [ObservableProperty]
    public partial string MapStructureFilePath { get; set; }
    [ObservableProperty]
    public partial string CellMetadataFilePath { get; set; }
}