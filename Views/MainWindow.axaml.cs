using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using GridMapper.ViewModels;

namespace GridMapper.Views;

public partial class MainWindow : Window
{
    public static readonly StyledProperty<DataBearer> BearerOutProperty =
        AvaloniaProperty.Register<MainWindow, DataBearer>(nameof(BearerOut));
    public DataBearer BearerOut
    {
        get => GetValue(BearerOutProperty);
        set => SetValue(BearerOutProperty, value);
    }
    public static readonly StyledProperty<IBrush> SaveUndoColorProperty = 
        AvaloniaProperty.Register<MainWindow, IBrush>(nameof(SaveUndoColor));
    public IBrush SaveUndoColor
    {
        get => GetValue(SaveUndoColorProperty);
        set => SetValue(SaveUndoColorProperty, value);
    }
    public MainWindow()
    {
        MainViewModel mvm = new MainViewModel();
        DataContext = mvm;
        BearerOut = mvm.BearerOut;
        SetSaveUndoColor();
        InitializeComponent();
        BearerOut.DataChanged += (changed) =>
        {
            SetSaveUndoColor();
        };
    }

    private void SetSaveUndoColor()
    {
        SaveUndoColor = (BearerOut.GetChanged() ? Brushes.LightGray : Brushes.DarkGray);
    }
}