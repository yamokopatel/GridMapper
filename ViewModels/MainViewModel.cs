using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GridMapper.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    #region FIELDS
    private DataBearer bearer;
    [ObservableProperty]
    public partial string Greeting { get; set; } = "Welcome to Avalonia!";
    #endregion
    
    #region CONSTRUCTOR
    public MainViewModel()
    {
        bearer = new DataBearer();

        #region Subscriptions
        bearer.SelectedMapChanged += (mapIndex) =>
        {
            OnSelectedMapChanged(mapIndex);
        };
        #endregion
    }
    #endregion

    #region SETTERS
    public void SetProcessingMap(int mapIndex)
    {
        bearer.SetProcessingMap(mapIndex);
    }
    #endregion

    #region EVENTS
    public event Action<int>? SelectedMapChanged;
    #endregion

    #region EVENT SENDERS
    protected virtual void OnSelectedMapChanged(int selectedMap)
    {
        SelectedMapChanged?.Invoke(selectedMap);
    }
    #endregion

    #region COMMANDS
    [RelayCommand]
    private void Save()
    {
        bearer.SaveData();
    }
    [RelayCommand]
    private void Undo()
    {
        bearer.AskLoadData();
    }
    #endregion
}
