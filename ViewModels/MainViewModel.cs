using CommunityToolkit.Mvvm.ComponentModel;

namespace GridMapper.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string Greeting { get; set; } = "Welcome to Avalonia!";

    private DataBearer bearer = new DataBearer();
    [ObservableProperty]
    public partial DataBearer BearerOut { get; set; } = new DataBearer();
}
