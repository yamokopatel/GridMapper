using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GridMapper.ViewModels.Components;

namespace GridMapper.Views.Components;

public partial class ButtonField : UserControl
{
    public static readonly StyledProperty<DataBearer> BearerInProperty =
        AvaloniaProperty.Register<ButtonField, DataBearer>(nameof(BearerIn));
    public DataBearer BearerIn
    {
        get => GetValue(BearerInProperty);
        set => SetValue(BearerInProperty, value);
    }
    public static readonly StyledProperty<DataBearer> BearerOutProperty =
        AvaloniaProperty.Register<ButtonField, DataBearer>(nameof(BearerOut));
    public DataBearer BearerOut
    {
        get => GetValue(BearerOutProperty);
        set => SetValue(BearerOutProperty, value);
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
            BearerOut = BearerIn;
        }
    }
}