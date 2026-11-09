using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GridMapper.ViewModels.Components;
using GridMapper.Views.Components;

namespace GridMapper.ViewModels.Tabs;

public partial class MapEditorViewModel : ViewModelBase
{
    #region CONSTRUCTOR
    public  MapEditorViewModel()
    {
        // BearerIn.FilesLoaded += () => 
        // {
        //     MapsToItems();
        // };
        // BearerIn.SelectedMapChanged += (index) =>
        // {
        //     if(index > -1)
        //     {
        //         SettingsVisible = true;
        //         MapBiomeID = (decimal?)BearerIn.GetMaps()![index].GetBiomeId();
        //         MapName = BearerIn.GetMaps()![index].GetMapName();
        //     }
        //     else
        //     {
        //         SettingsVisible = false;
        //         MapBiomeID = null;
        //         MapName = "";
        //     }
        // };
    }
    #endregion

    #region  FIELDS
    private bool ChangingMap = false;

    //  PROPERTIES
    [ObservableProperty]
    public partial ObservableCollection<ComboBoxItem> MapOptions { get; set; } = new ObservableCollection<ComboBoxItem>();
    [ObservableProperty]
    public partial int SelectedMap { get; set; } = -1;
    [ObservableProperty]
    public partial bool SettingsVisible { get; set; } = false;
    [ObservableProperty]
    public partial bool AbleToDelete { get; set; } = false;
    [ObservableProperty]
    public partial byte? MapID { get; set; } = null;
    [ObservableProperty]
    public partial decimal? MapBiomeID { get; set; } = null;
    [ObservableProperty]
    public partial string MapName { get; set; } = "";
    // [ObservableProperty]
    // public partial string DebugText { get; set; } = ""; // Extra field to see this view errors
    #endregion

    #region FIELD EVENTS
    partial void OnSelectedMapChanged(int value)
    {
        ChangingMap = true;
        // BearerIn.SetProcessingMap(value);
        // AbleToDelete = value > -1;
        // MapID = (value > -1) ? BearerIn.GetMaps()![value].GetMapId() : null;
        // MapBiomeID = (value > -1) ? BearerIn.GetMaps()![value].GetBiomeId() : null;
        // MapName = (value > -1) ? BearerIn.GetMaps()![value].GetMapName() : "";
        ChangingMap = false;
    }
    partial void OnMapBiomeIDChanged(decimal? value)
    {
        // if(SelectedMap != -1 && !ChangingMap)
        // {
        //     BearerIn.EditMap<byte>('b',(byte)value!.Value);
        //     BearerIn.SetChanged(true);
        // }
    }
    partial void OnMapNameChanged(string value)
    {
        // if(SelectedMap != -1 && !ChangingMap)
        // {
        //     BearerIn.EditMap<string>('n', value);
        //     RenameItem(value);
        //     BearerIn.SetChanged(true);
        // }
    }
    #endregion
    
    #region FUNCTIONS
    private void MapsToItems()
    {
        // MapOptions.Clear();
        // for(int a = 0; a < BearerIn.GetMaps()!.Count; a++)
        // {
        //     ComboBoxItem item = new ComboBoxItem();
        //     item.Content = $"{BearerIn.GetMaps()![a].GetMapName()}\t({BearerIn.GetMaps()![a].GetMapId()})";
        //     MapOptions.Add(item);
        // }
    }
    private void RenameItem(string name)
    {
        ComboBoxItem item = MapOptions[SelectedMap];
        string[] parts = item.Content!.ToString()!.Split("\t");
        parts[0] = name;
        string newName = $"{parts[0]}\t{parts[1]}";
        item.Content = newName;
    }
    #endregion
    
    #region COMMANDS
    [RelayCommand]
    private void NewMap()
    {
        // int newMapIndex = BearerIn.CreateMap();
        // MapsToItems();
        // SelectedMap = newMapIndex;
    }
    [RelayCommand]
    private void DeleteMap()
    {
        // SelectedMap = BearerIn.DeleteMap();
        // MapsToItems();
    }
    #endregion
    
    #region EVENTS
    public event Action<int>? OtherMapSelected;
    #endregion
    
    #region EVENT SENDERS
    protected virtual void OnOtherMapSelected(int mapIndex)
    {
        OtherMapSelected?.Invoke(mapIndex);
    }
    #endregion
}