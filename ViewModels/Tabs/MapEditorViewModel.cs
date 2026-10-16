using System;
using System.Collections.Generic;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GridMapper.ViewModels.Tabs;

public partial class MapEditorViewModel : ViewModelBase
{
    public DataBearer BearerIn;

    //  PROPERTIES
    [ObservableProperty]
    public partial List<ComboBoxItem> MapOptions { get; set; } = new List<ComboBoxItem>();
    [ObservableProperty]
    public partial int SelectedMap { get; set; } = -1;
    [ObservableProperty]
    public partial bool SettingsVisible { get; set; } = false;
    [ObservableProperty]
    public partial decimal? MapBiomeID { get; set; } = null;
    [ObservableProperty]
    public partial string MapName { get; set; } = "";
    // [ObservableProperty]
    // public partial string DebugText { get; set; } = ""; // Extra field to see this view errors

    public  MapEditorViewModel(DataBearer bearerIn)
    {
        BearerIn = bearerIn;
        // try
        // {
        BearerIn.FilesLoaded += () => 
        {
            MapsToItems();
        };
        BearerIn.SelectedMapChanged += (index) =>
        {
            if(index > -1)
            {
                SettingsVisible = true;
                MapBiomeID = (decimal?)BearerIn.GetMaps()![index].GetBiomeId();
                MapName = BearerIn.GetMaps()![index].GetMapName();
            }
            else
            {
                SettingsVisible = false;
                MapBiomeID = null;
                MapName = "";
            }
        };
    //     }
    //     catch(Exception e)
    //     {
    //         DebugText = e.ToString();
    //     }
    }

    [RelayCommand]
    private void NewMap()
    {
        
    }

    private void MapsToItems()
    {
        MapOptions.Clear();
        for(int a = 0; a < BearerIn.GetMaps()!.Count; a++)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Content = $"{BearerIn.GetMaps()![a].GetMapName()} ({BearerIn.GetMaps()![a].GetMapId()})";
            MapOptions.Add(item);
        }
    }
}