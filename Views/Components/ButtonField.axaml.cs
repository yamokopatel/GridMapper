using System.Collections.Generic;
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
    public static readonly StyledProperty<List<BorderedButton>> ButtonListProperty =
        AvaloniaProperty.Register<ButtonField, List<BorderedButton>>(nameof(ButtonList));
    public List<BorderedButton> ButtonList
    {
        get => GetValue(ButtonListProperty);
        set => SetValue(ButtonListProperty, value);
    }

    public ButtonField()
    {
        ButtonList = new List<BorderedButton>();
        InitializeComponent();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if(change.Property == BearerInProperty)
        {
            BearerOut = BearerIn;
            BearerIn.SelectedMapChanged += (mapIndex) =>
            {
                RefillField(mapIndex);
            };
        }
    }
    private void RefillField(int mapIndex)
    {
        int mapOffset = BearerIn.GetMaps()![mapIndex].GetByteOffset();
        List<BorderedButton> buttons = new List<BorderedButton>();
        for(int z = 0; z < 16; z++)
        {
            for(int x = 0; x < 16; x++)
            {
                BorderedButton button = new BorderedButton
                {
                    BearerIn = BearerOut,
                    CellIndex = (mapOffset + x*16 + x)
                };
                Grid.SetColumn(button,x);
                Grid.SetRow(button, z);
                buttons.Add(button);
            }
        }
        ButtonList = buttons;
    }
}