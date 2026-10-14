using Avalonia;
using Avalonia.Controls;
using GridMapper.ViewModels.Tabs;

namespace GridMapper.Views.Tabs;

public partial class MapEditor : UserControl
{
    public static readonly StyledProperty<DataBearer> BearerInProperty =
        AvaloniaProperty.Register<FileBrowser, DataBearer>(nameof(BearerIn));
    public DataBearer BearerIn
    {
        get => GetValue(BearerInProperty);
        set => SetValue(BearerInProperty, value);
    }
    private MapEditorViewModel mevm;
    public MapEditor()
    {
        mevm = new MapEditorViewModel();
        DataContext = mevm;
        InitializeComponent();
        mevm.BearerIn = BearerIn;
    }
}