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
        OnSelectedMapChanged(index);
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
    public int DeleteMap()
    {
        MapObjects!.RemoveAll(obj => obj.GetMapId() == Maps![MapInProcessingIndex].GetMapId());
        MapStructures!.RemoveRange(Maps![MapInProcessingIndex].GetByteOffset(), 256);
        List<Transition> transitionsWithoutNextMap = MapObjects.OfType<Transition>().ToList();
        for(int a = 0; a < transitionsWithoutNextMap.Count; a++)
        {
            transitionsWithoutNextMap[a].SetNextMapId(0);
            transitionsWithoutNextMap[a].SetNextX(0);
            transitionsWithoutNextMap[a].SetNextZ(0);
        }
        for(int a = MapInProcessingIndex + 1; a < Maps.Count; a++)
        {
            Maps[a].SetByteOffset(Maps[a].GetByteOffset() - 256);
        }
        Maps.RemoveAt(MapInProcessingIndex);
        return -1;
    }
    public void EditMap<T>(char field, T value)
    {
        switch (field)
        {
            case 'b':
                Maps![MapInProcessingIndex].SetBiomeId((byte)(object)value!);
                break;
            case 'n':
                Maps![MapInProcessingIndex].SetMapName((string)(object)value!);
                break;
            default:
                throw new ArgumentException("Attempt to edit not existed field");
        }
    }
    #endregion

    #region STRUCTURES
    public void EditCell(byte part, byte state)
    {
        byte[] parts = new byte[4];
        parts[0] = (byte)((MapStructures![CellInProcessingIndex] >> 6) & 0b11);
        parts[1] = (byte)((MapStructures![CellInProcessingIndex] >> 4) & 0b11);
        parts[2] = (byte)((MapStructures![CellInProcessingIndex] >> 2) & 0b11);
        parts[3] = (byte)(MapStructures![CellInProcessingIndex] & 0b11);
        parts[part] = state;
        byte cellState = (byte)((parts[0] * 64) + (parts[1] * 16) + (parts[2] * 4) + parts[3]);
        MapStructures![CellInProcessingIndex] = cellState;
        CellModified!(CellInProcessingIndex);
    }
    #endregion

    #region OBJECTS
    public int CreateObject<T>() where T : BaseObject
    {
        int m = CellInProcessingIndex / 256, x = CellInProcessingIndex % 256 % 16;
        int z = CellInProcessingIndex % 256 / 16, mid = Maps![m].GetMapId();
        List<ushort> occupedIds = new List<ushort>();
        for(int a = 0; a < MapObjects!.Count; a++)
        {
            occupedIds.Add(MapObjects![a].GetObjectId());
        }
        ushort? freeId = null;
        for(int a = 0; a < 65_536; a++)
        {
            if (!occupedIds.Contains((ushort)a))
            {
                freeId = (ushort)a;
                break;
            }
        }
        if(freeId != null)
        {
            BaseObject newObject = new BaseObject(freeId!.Value, (byte)mid, (byte)x, (byte)z);
            if(typeof(T) == typeof(Chest))
            {
                newObject = new Chest(freeId!.Value, (byte)mid, (byte)x, (byte)z, []);
            }
            else if(typeof(T) == typeof(Group))
            {
                newObject = new Group(freeId!.Value, (byte)mid, (byte)x, (byte)z, []);
            }
            else if(typeof(T) == typeof(Transition))
            {
                newObject = new Transition(freeId!.Value, (byte)mid, (byte)x, (byte)z, 0, 0, 0, new Direction());
            }
            MapObjects!.Add(newObject);
        }
        else
        {
            Notify("New map object will be out of limit.");
            return -1;
        }
        return MapObjects!.Count - 1;
    }
    public BaseObject ChangeObject<T>()
    {
        int m = CellInProcessingIndex / 256, x = CellInProcessingIndex % 256 % 16;
        int z = CellInProcessingIndex % 256 / 16, mid = Maps![m].GetMapId();
        int index = MapObjects!.FindIndex(obj => obj.GetMapId() == mid && obj.GetX() == x && obj.GetZ() == z);
        ushort oid = MapObjects![index].GetObjectId();
        BaseObject newObject = new BaseObject(0, 0, 0, 0);
        if(typeof(T) == typeof(Chest))
        {
            newObject = new Chest(oid, (byte)mid, (byte)x, (byte)z, []);
        }
        else if(typeof(T) == typeof(Group))
        {
            newObject = new Group(oid, (byte)mid, (byte)x, (byte)z, []);
        }
        else if(typeof(T) == typeof(Transition))
        {
            newObject = new Transition(oid, (byte)mid, (byte)x, (byte)z, 0, 0, 0, new Direction());
        }
        MapObjects![index] = newObject;
        return MapObjects![index];
    }
    public void DeleteObject()
    {
        int m = CellInProcessingIndex / 256, x = CellInProcessingIndex % 256 % 16;
        int z = CellInProcessingIndex % 256 / 16, mid = Maps![m].GetMapId();
        int index = MapObjects!.FindIndex(obj => obj.GetMapId() == mid && obj.GetX() == x && obj.GetZ() == z);
        MapObjects.RemoveAt(index);
    }

    #region SUBJECTS
    public int AddSubject()
    {
        Chest chest = GetChest();
        chest.GetSubjectIds().Add(0);
        return chest.GetSubjectIds().Count - 1;
    }
    public void SetSubject(int index, byte value)
    {
        Chest chest = GetChest();
        chest.GetSubjectIds()[index] = value;
    }
    public void RemoveSubject(int index)
    {
        Chest chest = GetChest();
        chest.GetSubjectIds().RemoveAt(index);
    }
    private Chest GetChest()
    {
        int m = CellInProcessingIndex / 256, x = CellInProcessingIndex % 256 % 16;
        int z = CellInProcessingIndex % 256 / 16, mid = Maps![m].GetMapId();
        return (Chest)MapObjects!.Find(c => c.GetMapId() == mid && c.GetX() == x && c.GetZ() == z)!;
    }
    #endregion

    #region ENEMIES
    public int AddEnemy()
    {
        Group group = GetGroup();
        group.GetEnemyIds().Add(0);
        return group.GetEnemyIds().Count - 1;
    }
    public void SetEnemy(int index, byte value)
    {
        Group group = GetGroup();
        group.GetEnemyIds()[index] = value;
    }
    public void RemoveEnemy(int index)
    {
        Group group = GetGroup();
        group.GetEnemyIds().RemoveAt(index);
    }
    private Group GetGroup()
    {
        int m = CellInProcessingIndex / 256, x = CellInProcessingIndex % 256 % 16;
        int z = CellInProcessingIndex % 256 / 16, mid = Maps![m].GetMapId();
        return (Group)MapObjects!.Find(c => c.GetMapId() == mid && c.GetX() == x && c.GetZ() == z)!;
    }
    #endregion

    #region Transition
    public void SetNMapId(byte nextId)
    {
        Transition tran = GetTransition();
        tran.SetNextMapId(nextId);
    }
    public void SetNX(byte nextX)
    {
        Transition tran = GetTransition();
        tran.SetNextX(nextX);
    }
    public void SetNZ(byte nextZ)
    {
        Transition tran = GetTransition();
        tran.SetNextZ(nextZ);
    }
    public void SetNDir(Direction nextDir)
    {
        Transition tran = GetTransition();
        tran.SetNextDirection(nextDir);
    }
    private Transition GetTransition()
    {
        int m = CellInProcessingIndex / 256, x = CellInProcessingIndex % 256 % 16;
        int z = CellInProcessingIndex % 256 / 16, mid = Maps![m].GetMapId();
        return (Transition)MapObjects!.Find(c => c.GetMapId() == mid && c.GetX() == x && c.GetZ() == z)!;
    }
    #endregion
    #endregion
}