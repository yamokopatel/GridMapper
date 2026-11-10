using System;
using System.Collections.Generic;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GridMapper.ViewModels.Tabs;

public partial class StructureEditorViewModel : ViewModelBase
{
    #region PROPERTIES
    [ObservableProperty]
    public partial List<ComboBoxItem> MapOptions { get; set; } = new List<ComboBoxItem>();
    [ObservableProperty]
    public partial int SelectedMap { get; set; } = -1;
    [ObservableProperty]
    public partial ComboBoxItem[] WestWallTypes { get; set; } = [
        new ComboBoxItem{Content = "Empty"},
        new ComboBoxItem{Content = "Wall"},
        new ComboBoxItem{Content = "Door"},
        new ComboBoxItem{Content = "Secret door"}
    ];
    [ObservableProperty]
    public partial ComboBoxItem[] NorthWallTypes { get; set; } = [
        new ComboBoxItem{Content = "Empty"},
        new ComboBoxItem{Content = "Wall"},
        new ComboBoxItem{Content = "Door"},
        new ComboBoxItem{Content = "Secret door"}
    ];
    [ObservableProperty]
    public partial ComboBoxItem[] EastWallTypes { get; set; } = [
        new ComboBoxItem{Content = "Empty"},
        new ComboBoxItem{Content = "Wall"},
        new ComboBoxItem{Content = "Door"},
        new ComboBoxItem{Content = "Secret door"}
    ];
    [ObservableProperty]
    public partial ComboBoxItem[] SouthWallTypes { get; set; } = [
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
    #endregion
    
    #region PROPERTY EVENTS
    partial void OnSelectedMapChanged(int value)
    {
        OnOtherMapSelected(value);
    }
    partial void OnWestTypeIndexChanged(int value)
    {
        // BearerIn.EditCell(0, (byte)value);
    }
    partial void OnNorthTypeIndexChanged(int value)
    {
        // BearerIn.EditCell(1, (byte)value);
    }
    partial void OnEastTypeIndexChanged(int value)
    {
        // BearerIn.EditCell(2, (byte)value);
    }
    partial void OnSouthTypeIndexChanged(int value)
    {
        // BearerIn.EditCell(3, (byte)value);
    }
    #endregion
    
    #region CONSTRUCTOR
    public StructureEditorViewModel()
    {
        // BearerIn.FilesLoaded += () => 
        // {
        //     MapsToItems();
        // };
    }
    #endregion

    #region SETTERS
    public void SetSelectedMap(int index)
    {
        if(SelectedMap != index)
        {
            SelectedMap = index;
        }
    }
    #endregion

    #region FUNCTIONS
    private void MapsToItems()
    {
        // MapOptions.Clear();
        // for(int a = 0; a < BearerIn.GetMaps()!.Count; a++)
        // {
        //     ComboBoxItem item = new ComboBoxItem();
        //     item.Content = $"{BearerIn.GetMaps()![a].GetMapName()} ({BearerIn.GetMaps()![a].GetMapId()})";
        //     MapOptions.Add(item);
        // }
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