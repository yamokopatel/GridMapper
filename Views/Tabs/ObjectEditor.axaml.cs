using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GridMapper.ViewModels.Tabs;

namespace GridMapper.Views.Tabs;

public partial class ObjectEditor : UserControl
{
    private ObjectEditorViewModel oevm = null!;
    public static readonly StyledProperty<DataBearer> BearerInProperty =
        AvaloniaProperty.Register<FileBrowser, DataBearer>(nameof(BearerIn));
    public DataBearer BearerIn
    {
        get => GetValue(BearerInProperty);
        set => SetValue(BearerInProperty, value);
    }
    public ObjectEditor()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if(change.Property == BearerInProperty)
        {
            oevm = new ObjectEditorViewModel(BearerIn);
            DataContext = oevm;
        }
    }
}