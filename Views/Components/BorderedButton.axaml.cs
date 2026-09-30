using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace GridMapper.Views.Components;

public class BorderedButton : Button
{
    public static readonly StyledProperty<byte> CellByteProperty = AvaloniaProperty.Register<BorderedButton, byte>(nameof(CellByte));
    public byte CellByte
    {
        get => GetValue(CellByteProperty);
        set => SetValue(CellByteProperty, value);
    }
    public BorderedButton()
    {
        var content = Content;
        DockPanel panel = new DockPanel();

        panel.Children.Add(new ContentPresenter{Content = content});

        panel.Children.Add(new Border{
            [DockPanel.DockProperty] = Dock.Left, 
            BorderThickness = new Thickness(1,0,0,0), 
            BorderBrush = BorderColor((byte)((CellByte >> 6) & 0b11))});
        panel.Children.Add(new Border{
            [DockPanel.DockProperty] = Dock.Top, 
            BorderThickness = new Thickness(0,1,0,0), 
            BorderBrush = BorderColor((byte)((CellByte >> 4) & 0b11))});
        panel.Children.Add(new Border{
            [DockPanel.DockProperty] = Dock.Right, 
            BorderThickness = new Thickness(0,0,1,0), 
            BorderBrush = BorderColor((byte)((CellByte >> 2) & 0b11))});
        panel.Children.Add(new Border{
            [DockPanel.DockProperty] = Dock.Bottom, 
            BorderThickness = new Thickness(0,0,0,1), 
            BorderBrush = BorderColor((byte)(CellByte & 0b11))});

        Content = panel;
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