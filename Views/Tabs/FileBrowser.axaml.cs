using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GridMapper.Views.Tabs;

public partial class FileBrowser : UserControl
{
    //  PARAMETERS
    //  map metadata file path
    public static readonly StyledProperty<string> MapMetadataFilePathProperty = 
        AvaloniaProperty.Register<FileBrowser, string>(nameof(MapMetadataFilePath));
    public string MapMetadataFilePath
    {
        get => GetValue(MapMetadataFilePathProperty);
        set => SetValue(MapMetadataFilePathProperty, value);
    }
    //  map strusture file path
    public static readonly StyledProperty<string> MapStructureFilePathProperty =
        AvaloniaProperty.Register<FileBrowser, string>(nameof(MapStructureFilePath));
    public string MapStructureFilePath
    {
        get => GetValue(MapStructureFilePathProperty);
        set => SetValue(MapStructureFilePathProperty, value);
    }
    //  cell metadata file path
    public static readonly StyledProperty<string> CellMetadataFilePathProperty =
        AvaloniaProperty.Register<FileBrowser, string>(nameof(CellMetadataFilePath));
    public string CellMetadataFilePath
    {
        get => GetValue(CellMetadataFilePathProperty);
        set => SetValue(CellMetadataFilePathProperty, value);
    }

    //  CONSTRUCTOR
    public FileBrowser()
    {
        MapMetadataFilePath = ""; MapStructureFilePath = ""; CellMetadataFilePath = "";
        InitializeComponent();
    }
}