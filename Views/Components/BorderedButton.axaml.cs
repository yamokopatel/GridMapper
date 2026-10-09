using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace GridMapper.Views.Components;

public partial class BorderedButton : UserControl
{
    //  PARAMETERS
    //  cell byte
    public static readonly StyledProperty<byte> CellByteProperty = 
        AvaloniaProperty.Register<BorderedButton, byte>(nameof(CellByte));
    public byte CellByte
    {
        get => GetValue(CellByteProperty);
        set => SetValue(CellByteProperty, value);
    }
    //  left border color
    public static readonly StyledProperty<IBrush> LeftBorderBrushProperty =
        AvaloniaProperty.Register<BorderedButton, IBrush>(nameof(LeftBorderBrush));
    
    public IBrush LeftBorderBrush
    {
        get => GetValue(LeftBorderBrushProperty); 
        set => SetValue(LeftBorderBrushProperty, value); 
    }
    //  top border color
    public static readonly StyledProperty<IBrush> TopBorderBrushProperty = 
        AvaloniaProperty.Register<BorderedButton, IBrush>(nameof(TopBorderBrush));
    public IBrush TopBorderBrush
    { 
        get => GetValue(TopBorderBrushProperty); 
        set => SetValue(TopBorderBrushProperty, value); 
    }
    //  right border color
    public static readonly StyledProperty<IBrush> RightBorderBrushProperty =
        AvaloniaProperty.Register<BorderedButton, IBrush>(nameof(RightBorderBrush));
    public IBrush RightBorderBrush
    { 
        get => GetValue(RightBorderBrushProperty); 
        set => SetValue(RightBorderBrushProperty, value); 
    }
    //  bottom border color
    public static readonly StyledProperty<IBrush> BottomBorderBrushProperty =
        AvaloniaProperty.Register<BorderedButton, IBrush>(nameof(BottomBorderBrush));
    public IBrush BottomBorderBrush
    { 
        get => GetValue(BottomBorderBrushProperty); 
        set => SetValue(BottomBorderBrushProperty, value); 
    }

    //  CONSTRUCTOR
    public BorderedButton()
    {
        InitializeComponent();
    }

    //  EVENT LISTENER
    //  value changed
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if(change.Property == CellByteProperty)
        {
            SetBorderColors();
        }
    }

    //  FUNCTIONS
    private void SetBorderColors()
    {
        LeftBorderBrush = BorderColor((byte)((CellByte >> 6) & 0b11));
        TopBorderBrush = BorderColor((byte)((CellByte >> 4) & 0b11));
        RightBorderBrush = BorderColor((byte)((CellByte >> 2) & 0b11));
        BottomBorderBrush = BorderColor((byte)(CellByte & 0b11));
    }
    public byte GetCellByte() => CellByte;
    public void SetCellByte(byte cellByte){CellByte = cellByte;}
    private IBrush BorderColor(byte value)
    {
        switch (value)
        {
            case 0: return Brushes.White;
            case 1: return Brushes.Brown;
            case 2: return Brushes.Cyan;
            case 3: return Brushes.Violet;
            default: return Brushes.White;
        }
    }
}