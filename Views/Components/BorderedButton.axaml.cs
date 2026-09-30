using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace GridMapper.Views.Components;

public partial class BorderedButton : UserControl
{
    public static readonly StyledProperty<byte> CellByteProperty = AvaloniaProperty.Register<BorderedButton, byte>(nameof(CellByte));
    public byte CellByte
    {
        get => GetValue(CellByteProperty);
        set => SetValue(CellByteProperty, value);
    }
    public IBrush LeftBorderBrush{ get; set; }
    public IBrush TopBorderBrush{ get; set; }
    public IBrush RightBorderBrush{ get; set; }
    public IBrush BottomBorderBrush{ get; set; }
    public BorderedButton()
    {
        // Console.Error.WriteLine($"BORDERED BUTTON CREATED: {CellByte}");
        // throw new Exception("BORDERED BUTTON CREATED");
        // LeftBorderBrush = BorderColor((byte)((CellByte >> 6) & 0b11));
        LeftBorderBrush = Brushes.Brown;
        // TopBorderBrush = BorderColor((byte)((CellByte >> 4) & 0b11));
        TopBorderBrush = Brushes.Brown;
        // RightBorderBrush = BorderColor((byte)((CellByte >> 2) & 0b11));
        RightBorderBrush = Brushes.White;
        // BottomBorderBrush = BorderColor((byte)(CellByte & 0b11));
        BottomBorderBrush = Brushes.Cyan;
        InitializeComponent();
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