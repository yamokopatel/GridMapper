using System.Collections.Generic;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace GridMapper.ViewModels.Tabs;

public partial class MapEditorViewModel : ViewModelBase
{
    public DataBearer? BearerIn;

    //  PROPERTIES
    [ObservableProperty]
    public partial List<ComboBoxItem> MapOptions { get; set; } = new List<ComboBoxItem>();
    [ObservableProperty]
    public partial bool SettingsVisible { get; set; } = false;

    public MapEditorViewModel()
    {
        BearerIn!.FilesLoaded += () => {
            MapsToItems();
        };
    }

    [RelayCommand]
    private void NewMap()
    {
        
    }

    private void MapsToItems()
    {
        MapOptions.Clear();
        for(int a = 0; a < BearerIn!.GetMaps()!.Count; a++)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Content = $"{BearerIn.GetMaps()![a].GetMapName()} ({BearerIn.GetMaps()![a].GetMapId()})";
            MapOptions.Add(item);
        }
    }
}