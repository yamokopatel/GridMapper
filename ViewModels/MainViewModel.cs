using System;
using System.Collections.Generic;
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
        bearer.FilesLoaded += (maps) =>
        {
            OnMapsLoaded(maps);
        };
        bearer.SelectedMapChanged += (mapIndex) =>
        {
            OnSelectedMapChanged(mapIndex);
        };
        bearer.MapCreatedOrDeleted += () =>
        {
            OnMapCreatedOrDeleted();
        };
        #endregion
    }
    #endregion

    #region SETTERS
    public void SetProcessingMap(int mapIndex)
    {
        bearer.SetProcessingMap(mapIndex);
    }
    #region Paths
    public void SetMapMetaPath(string path)
    {
        bearer.SetMapMetaPath(path);
    }
    public void SetStructurePath(string path)
    {
        bearer.SetStructurePath(path);
    }
    public void SetCellMetaPath(string path)
    {
        bearer.SetCellMetaPath(path);
    }
    #endregion
    public void SetChanged(bool changed)
    {
        bearer.SetChanged(changed);
    }
    #endregion

    #region FUNCTIONS
    public void CreateMap()
    {
        bearer.CreateMap();
    }
    public void DeleteMap()
    {
        bearer.DeleteMap();
    }
    #endregion

    #region EVENTS
    public event Action<List<Map>>? MapsLoaded;
    public event Action<int>? SelectedMapChanged;
    public event Action<bool>? DataChanged;
    public event Action? MapCreatedOrDeleted;
    #endregion

    #region EVENT SENDERS
    protected virtual void OnMapsLoaded(List<Map> maps)
    {
        MapsLoaded?.Invoke(maps);
    }
    protected virtual void OnSelectedMapChanged(int selectedMap)
    {
        SelectedMapChanged?.Invoke(selectedMap);
    }
    protected virtual void OnDataChanged(bool changed)
    {
        DataChanged?.Invoke(changed);
    }
    protected virtual void OnMapCreatedOrDeleted()
    {
        MapCreatedOrDeleted?.Invoke();
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
