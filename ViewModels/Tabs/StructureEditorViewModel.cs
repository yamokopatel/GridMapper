using System.Collections.Generic;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GridMapper.ViewModels.Tabs;

public partial class StructureEditorViewModel : ViewModelBase
{
    private DataBearer BearerIn;
    //  PROPERTIES
    [ObservableProperty]
    public partial List<ComboBoxItem> MapOptions { get; set; } = new List<ComboBoxItem>();
    [ObservableProperty]
    public partial int SelectedMap { get; set; } = -1;
    partial void OnSelectedMapChanged(int value)
    {
        BearerIn.SetProcessingMap(value);
    }
    [ObservableProperty]
    public partial ComboBoxItem[] WallTypes { get; set; } = [
        new ComboBoxItem{Content = "Empty"},
        new ComboBoxItem{Content = "Wall"},
        new ComboBoxItem{Content = "Door"},
        new ComboBoxItem{Content = "Secret door"}
    ];
    [ObservableProperty]
    public partial int WestTypeIndex { get; set; } = 0;
    [ObservableProperty]
    public partial int NorthTypeIndex { get; set; } = 0;
    [ObservableProperty]
    public partial int EastTypeIndex { get; set; } = 0;
    [ObservableProperty]
    public partial int SouthTypeIndex { get; set; } = 0;

    public StructureEditorViewModel(DataBearer bearer)
    {
        BearerIn = bearer;

        BearerIn.FilesLoaded += () => 
        {
            MapsToItems();
        };
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