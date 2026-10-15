using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GridMapper.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string Greeting { get; set; } = "Welcome to Avalonia!";

    public DataBearer BearerOut = new DataBearer();

    [RelayCommand]
    private void Save()
    {
        BearerOut.SaveData();
    }
    [RelayCommand]
    private void Undo()
    {
        BearerOut.TryLoadData();
    }
}
