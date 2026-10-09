using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GridMapper.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string Greeting { get; set; } = "Welcome to Avalonia!";

    public DataBearer BearerOut  = new DataBearer();
}
