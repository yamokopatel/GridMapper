using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GridMapper.ViewModels.Components;

namespace GridMapper.Views.Components;

public partial class ButtonField : UserControl
{
    private ButtonFieldViewModel bfvm = null!;
    public static readonly StyledProperty<DataBearer> BearerInProperty =
        AvaloniaProperty.Register<ButtonField, DataBearer>(nameof(BearerIn));
    public DataBearer BearerIn
    {
        get => GetValue(BearerInProperty);
        set => SetValue(BearerInProperty, value);
    }

    public ButtonField()
    {
        InitializeComponent();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if(change.Property == BearerInProperty)
        {
            bfvm = new ButtonFieldViewModel(BearerIn);
            DataContext = bfvm;
        }
    }
}