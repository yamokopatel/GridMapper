using Avalonia;
using Avalonia.Controls;
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
    public MainWindow()
    {
        MainViewModel mvm = new MainViewModel();
        DataContext = mvm;
        BearerOut = mvm.BearerOut;
        InitializeComponent();
    }
}