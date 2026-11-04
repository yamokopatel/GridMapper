using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using GridMapper.ViewModels.Components;
using GridMapper.Views.Components;

public class DataBearer
{
    //  CONSTRUCTOR
    public DataBearer()
    {
        IsChanged = false;
        fileBearer = new FileBearer(this);

        //  Subscriptions
        fileBearer.MapsLoaded += (maps) =>
        {
            Maps = maps;
            if(Maps != null && MapStructures != null && MapObjects != null)
            {
                FilesLoaded!();
            }
        };
        fileBearer.StructuresLoaded += (strustures) =>
        {
            MapStructures = strustures;
            if(Maps != null && MapStructures != null && MapObjects != null)
            {
                FilesLoaded!();
            }
        };
        fileBearer.ObjectsLoaded += (objects) =>
        {
            MapObjects = objects;
            if(Maps != null && MapStructures != null && MapObjects != null)
            {
                FilesLoaded!();
            }
        };
    }

    #region   PARAMETERS
    //  Eventable
    private bool IsChanged;
    private int MapInProcessingIndex = -1;
    private int CellInProcessingIndex = -1;
    //  File data
    private List<Map>? Maps;
    private List<byte>? MapStructures;
    private List<BaseObject>? MapObjects;
    //  FileBearer
    private FileBearer fileBearer;
    #endregion

    #region GETTERS
    public bool GetChanged() => IsChanged;
    //  Data
    public List<Map>? GetMaps() => Maps;
    public List<byte>? GetStructures() => MapStructures;
    public List<BaseObject>? GetMapObjects() => MapObjects;
    #endregion

    #region SETTERS
    public void SetChanged(bool changed){
        IsChanged = changed;
        OnDataChanged(changed);
    }
    public void SetProcessingMap(int index)
    {
        MapInProcessingIndex = index;
        SelectedMapChanged!(index);
    }
    public void SetProcessingCell(int index)
    {
        CellInProcessingIndex = index;
        OnSelectedCellChanged(index);
    }
    public void SetSelectedCelWalls(byte w, byte n, byte e, byte s)
    {
        byte cellState = (byte)((w<<6) | (n<<4) | (e<<2) | s);
        if(CellInProcessingIndex > -1)
        {
            MapStructures![CellInProcessingIndex] = cellState;
            OnCellModified(CellInProcessingIndex);
        }
    }
    public void SetSelectedCellObject(BaseObject? mapObject)
    {
        if(mapObject == null)
        {
            MapObjects!.RemoveAt(CellInProcessingIndex);
            CellInProcessingIndex = -1;
            OnSelectedCellChanged(CellInProcessingIndex);
            OnCellModified(CellInProcessingIndex);
        }
        else
        {
            MapObjects![CellInProcessingIndex] = mapObject;
            OnCellModified(CellInProcessingIndex);
        }
    }
    #endregion

    #region FUNCTIONS
    private void Notify(string text)
    {
        NoticeWindow notice = new NoticeWindow();
        NoticeWindowViewModel nwvm = new NoticeWindowViewModel(text);
        notice.DataContext = nwvm;
        notice.Show();
    }
    public void SaveData()
    {
        fileBearer.SaveData(Maps, MapStructures, MapObjects);
    }
    public void AskLoadData()
    {
        fileBearer.TryLoadData();
    }
    #endregion

    #region EVENT SENDERS
    //  Changed
    public event Action<bool>? DataChanged;
    protected virtual void OnDataChanged(bool isChanged)
    {
        DataChanged?.Invoke(isChanged);
    }
    //  Map
    public event Action<int>? SelectedMapChanged;
    protected virtual void OnSelectedMapChanged(int selectedMap)
    {
        SelectedMapChanged?.Invoke(selectedMap);
    }
    //  Cell
    public event Action<int>? SelectedCellChanged;
    protected virtual void OnSelectedCellChanged(int selectedCell)
    {
        SelectedCellChanged?.Invoke(selectedCell);
    }
    //  Files Loaded
    public event Action? FilesLoaded;
    protected virtual void OnFilesLoaded()
    {
        FilesLoaded?.Invoke();
    }
    public event Action<int>? CellModified;
    protected virtual void OnCellModified(int cellIndex)
    {
        CellModified?.Invoke(cellIndex);
    }
    #endregion

    #region FILES
    public void SetMapMetaPath(string path)
    {
        fileBearer.SetMapMetaPath(path);
    }
    public void SetStructurePath(string path)
    {
        fileBearer.SetStructurePath(path);
    }
    public void SetCellMetaPath(string path)
    {
        fileBearer.SetCellMetaPath(path);
    }
    #endregion

    #region MAPS
    public int CreateMap()
    {
        int index = -1;
        List<int> occupedIds = new List<int>();
        byte? freeId = null;
        for(int a = 0; a < Maps!.Count; a++)
        {
            occupedIds.Add(Maps![a].GetMapId());
        }
        for(int a = 0; a < 256; a++)
        {
            if (!occupedIds.Contains(a))
            {
                freeId = (byte)a;
                break;
            }
        }
        if(freeId != null)
        {
            byte newId = freeId.Value;
            int newOffset = 0; 
            if(Maps!.Count > 0)
            {
                newOffset = Maps![^1].GetByteOffset() + 256;
            }
            Map newMap = new Map(newId, 0, newOffset, "");
            Maps!.Add(newMap);
            MapStructures!.AddRange(Enumerable.Repeat<byte>(0, 256));
            SetChanged(true);
            index = Maps!.Count - 1;
        }
        else
        {
            Notify("Can not create new map. Map limit is reached.");
        }
        SetProcessingMap(index);
        return index;
    }
    #endregion
}