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
    public DataBearer BearerIn;
    private bool ChangingMap = false;

    //  PROPERTIES
    [ObservableProperty]
    public partial ObservableCollection<ComboBoxItem> MapOptions { get; set; } = new ObservableCollection<ComboBoxItem>();
    [ObservableProperty]
    public partial int SelectedMap { get; set; } = -1;
    partial void OnSelectedMapChanged(int value)
    {
        ChangingMap = true;
        BearerIn.SetProcessingMap(value);
        AbleToDelete = value > -1;
        MapID = (value > -1) ? BearerIn.GetMaps()![value].GetMapId() : null;
        MapBiomeID = (value > -1) ? BearerIn.GetMaps()![value].GetBiomeId() : null;
        MapName = (value > -1) ? BearerIn.GetMaps()![value].GetMapName() : "";
        ChangingMap = false;
    }
    [ObservableProperty]
    public partial bool SettingsVisible { get; set; } = false;
    [ObservableProperty]
    public partial bool AbleToDelete { get; set; } = false;
    [ObservableProperty]
    public partial byte? MapID { get; set; } = null;
    [ObservableProperty]
    public partial decimal? MapBiomeID { get; set; } = null;
    partial void OnMapBiomeIDChanged(decimal? value)
    {
        if(SelectedMap != -1 && !ChangingMap)
        {
            BearerIn.GetMaps()![SelectedMap].SetBiomeId((byte)value!.Value);
            BearerIn.SetChanged(true);
        }
    }
    [ObservableProperty]
    public partial string MapName { get; set; } = "";
    partial void OnMapNameChanged(string value)
    {
        if(SelectedMap != -1 && !ChangingMap)
        {
            BearerIn.GetMaps()![SelectedMap].SetMapName(value);
            RenameItem(value);
            BearerIn.SetChanged(true);
        }
    }
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
        List<int> existedIds = new List<int>();
        byte? freeId = null;
        for(int a = 0; a < BearerIn.GetMaps()!.Count; a++)
        {
            existedIds.Add(BearerIn.GetMaps()![a].GetMapId());
        }
        for(int a = 0; a < 256; a++)
        {
            if (!existedIds.Contains(a))
            {
                freeId = (byte)a;
                break;
            }
        }
        if(freeId != null)
        {
            byte newId = freeId.Value;
            int newOffset = 0; 
            if(BearerIn.GetMaps()!.Count > 0)
            {
                newOffset = BearerIn.GetMaps()![^1].GetByteOffset() + 256;
            }
            Map newMap = new Map(newId, 0, newOffset, "");
            BearerIn.GetMaps()!.Add(newMap);
            BearerIn.GetStructures()!.AddRange(Enumerable.Repeat<byte>(0, 256));
            BearerIn.SetChanged(true);
            MapsToItems();
            SelectedMap = MapOptions.Count - 1;
        }
        else
        {
            NoticeWindow notice = new NoticeWindow();
            NoticeWindowViewModel nwvm = new NoticeWindowViewModel("Can not create new map. Map limit is reached.");
            notice.DataContext = nwvm;
        }
    }
    [RelayCommand]
    private void DeleteMap()
    {
        int deletable = SelectedMap;
        SelectedMap = -1;
        BearerIn.GetStructures()!.RemoveRange(BearerIn.GetMaps()![deletable].GetByteOffset(), 256);
        List<BaseObject> objectsWithoutMap = BearerIn.GetMapObjects()!.FindAll(owm => owm.GetMapId() == deletable);
        for(int a = 0; a < objectsWithoutMap.Count; a++)
        {
            BearerIn.GetMapObjects()!.Remove(objectsWithoutMap[a]);
        }
        for(int a = deletable; a < BearerIn.GetMaps()!.Count; a++)
        {
            BearerIn.GetMaps()![a].SetByteOffset(BearerIn.GetMaps()![a].GetByteOffset() - 256);
        }
        BearerIn.GetMaps()!.RemoveAt(deletable);
        SelectedMap =   ((BearerIn.GetMaps()!.Count >= deletable) 
                        ? deletable 
                        : deletable - 1);
    }

    private void MapsToItems()
    {
        MapOptions.Clear();
        for(int a = 0; a < BearerIn.GetMaps()!.Count; a++)
        {
            ComboBoxItem item = new ComboBoxItem();
            item.Content = $"{BearerIn.GetMaps()![a].GetMapName()}\t({BearerIn.GetMaps()![a].GetMapId()})";
            MapOptions.Add(item);
        }
    }
    private void RenameItem(string name)
    {
        ComboBoxItem item = MapOptions[SelectedMap];
        string[] parts = item.Content!.ToString()!.Split("\t");
        parts[0] = name;
        string newName = $"{parts[0]}\t{parts[1]}";
        item.Content = newName;
    }
}