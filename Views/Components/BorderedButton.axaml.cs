using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;

namespace GridMapper.Views.Components;

public partial class BorderedButton : Button
{
    public static readonly StyledProperty<byte> CellByteProperty = AvaloniaProperty.Register<BorderedButton, byte>(nameof(CellByte));
    public byte CellByte
    {
        get => GetValue(CellByteProperty);
        set => SetValue(CellByteProperty, value);
    }
    public BorderedButton()
    {
        DockPanel panel = new DockPanel();
        panel.Children.Add(new Border{
            [DockPanel.DockProperty] = Dock.Left, 
            BorderThickness = new Thickness(1,0,0,0), 
            BorderBrush = ((CellByte & 0b11) == 0 ? Brushes.White : 
                ((CellByte & 0b11) == 1 ? Brushes.Brown : 
                ((CellByte & 0b11) == 2 ? Brushes.Cyan : Brushes.Violet)))});
        panel.Children.Add(new Border{
            [DockPanel.DockProperty] = Dock.Top, 
            BorderThickness = new Thickness(0,1,0,0), 
            BorderBrush = ((CellByte >> 2 & 0b11) == 0 ? Brushes.White : 
                ((CellByte >> 2 & 0b11) == 1 ? Brushes.Brown : 
                ((CellByte >> 2 & 0b11) == 2 ? Brushes.Cyan : Brushes.Violet)))});
        panel.Children.Add(new Border{
            [DockPanel.DockProperty] = Dock.Right, 
            BorderThickness = new Thickness(0,0,1,0), 
            BorderBrush = ((CellByte >> 4 & 0b11) == 0 ? Brushes.White : 
                ((CellByte >> 4 & 0b11) == 1 ? Brushes.Brown : 
                ((CellByte >> 4 & 0b11) == 2 ? Brushes.Cyan : Brushes.Violet)))});
        panel.Children.Add(new Border{
            [DockPanel.DockProperty] = Dock.Bottom, 
            BorderThickness = new Thickness(0,0,0,1), 
            BorderBrush = ((CellByte >> 6 & 0b11) == 0 ? Brushes.White : 
                ((CellByte >> 6 & 0b11) == 1 ? Brushes.Brown : 
                ((CellByte >> 6 & 0b11) == 2 ? Brushes.Cyan : Brushes.Violet)))});
    }

    public byte GetCellByte() => CellByte;
    public void SetCellByte(byte cellByte){CellByte = cellByte;}
}