using Avalonia;
using Avalonia.Controls;
using GridMapper.ViewModels.Tabs;

namespace GridMapper.Views.Tabs;

public partial class StructureEditor : UserControl
{
    public static readonly StyledProperty<DataBearer> BearerInProperty =
        AvaloniaProperty.Register<FileBrowser, DataBearer>(nameof(BearerIn));
    public DataBearer BearerIn
    {
        get => GetValue(BearerInProperty);
        set => SetValue(BearerInProperty, value);
    }
    private StructureEditorViewModel sevm = null!;

    public StructureEditor()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if(change.Property == BearerInProperty)
        {
            sevm = new StructureEditorViewModel(BearerIn);
            DataContext = sevm;
        }
    }
}