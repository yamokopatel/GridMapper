using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Interactivity;
using System;
using Avalonia.Platform;
using Avalonia.Media.Imaging;

namespace GridMapper.Views.Components;

public partial class BorderedButton : UserControl
{
    private byte CellByte = 0;
    //  PARAMETERS
    //  data bearer
    public static readonly StyledProperty<DataBearer> BearerInProperty =
        AvaloniaProperty.Register<BorderedButton, DataBearer>(nameof(BearerIn));
    public DataBearer BearerIn
    {
        get => GetValue(BearerInProperty);
        set => SetValue(BearerInProperty, value);
    }
    //  cell index
    public static readonly StyledProperty<int> CellIndexProperty = 
        AvaloniaProperty.Register<BorderedButton, int>(nameof(CellIndex));
    public int CellIndex
    {
        get => GetValue(CellIndexProperty);
        set => SetValue(CellIndexProperty, value);
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
    //  button image
    public static readonly StyledProperty<IImage?> ButtonImageProperty = 
        AvaloniaProperty.Register<BorderedButton, IImage?>(nameof(ButtonImage));
    public IImage? ButtonImage
    {
        get => GetValue(ButtonImageProperty);
        set => SetValue(ButtonImageProperty, value);
    }

    //  CONSTRUCTOR
    public BorderedButton()
    {
        InitializeComponent();
    }

    //  EVENT LISTENER
    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if(change.Property == BearerInProperty)
        {
            if(CellIndexProperty != null)
            {
                BearerIn.CellModified += (cellIndex) =>
                {
                    if(cellIndex == CellIndex)
                    {
                        CellByte = BearerIn.GetStructures()![CellIndex];
                        SetBorderColors();
                        ButtonImage = objectIcon();
                    }
                };
            }
        }
        if(change.Property == CellIndexProperty)
        {
            if(BearerInProperty != null)
            {
                CellByte = BearerIn.GetStructures()![CellIndex];
                SetBorderColors();
                ButtonImage = objectIcon();
                
                BearerIn.CellModified += (cellIndex) =>
                {
                    if(cellIndex == CellIndex)
                    {
                        CellByte = BearerIn.GetStructures()![CellIndex];
                        SetBorderColors();
                        ButtonImage = objectIcon();
                    }
                };
            }
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
    private Bitmap? objectIcon()
    {
        int m = CellIndex / 256, x = CellIndex % 256 % 16, z = CellIndex % 256 / 16;
        byte mid = BearerIn.GetMaps()![m].GetMapId();
        switch (BearerIn.GetMapObjects()!.Find
            (o => o.GetMapId() == mid 
            && o.GetX() == x
            && o.GetZ() == z))
        {
            case Chest: return new Bitmap(AssetLoader.Open(new Uri("/Assets/Chest.png")));
            case Group: return new Bitmap(AssetLoader.Open(new Uri("/Assets/Group.png")));
            case Transition: return new Bitmap(AssetLoader.Open(new Uri("/Assets/Transition.png")));
            default: return null;
        }
    }

    //  Select Cell
    private void Cell_Select(object sender, RoutedEventArgs args)
    {
        BearerIn.SetProcessingCell(CellIndex);
    }
}