using System.IO;
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
}