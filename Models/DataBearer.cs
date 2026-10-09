using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class DataBearer
{
    private const ushort VERSION = 0x7D_01;
    //  PARAMETERS
    //  Eventable
    private bool IsChanged = false;
    private int MapInProcessingIndex = 0;
    private int CellInProcessingIndex = 0;
    private int ObjectInProcessingIndex = 0;
    //  File Paths
    private string? MapMetadataFilePath;
    private string? MapStructureFilePath;
    private string? CellMetadataFilePath;
    //  Files
    private FileInfo? MapMetadataFile;
    private FileInfo? MapStructureFile;
    private FileInfo? CellMetadataFile;
    //  File data
    private List<Map>? Maps;
    private List<byte>? MapStructures;
    private List<BaseObject>? MapObjects;

    //  CONSTRUCTOR
    public DataBearer(){}

    // GETTERS
    public bool GetChanged() => IsChanged;
    //  Paths
    public string? GetMapMetadataPath() => MapMetadataFilePath;
    public string? GetMapStructurePath() => MapStructureFilePath;
    public string? GetCellMetadataPath() => CellMetadataFilePath;
    //  Data
    public List<Map>? GetMaps() => Maps;
    public List<byte>? GetStructures() => MapStructures;
    public List<BaseObject>? GetMapObjects() => MapObjects;

    //  SETTERS
    public void SetChanged(bool changed){
        IsChanged = changed;
        OnDataChanged(changed);
    }
    //  Generifier?
    private bool CheckPathNotNull(ref string? path, string newPath)
    {
        if(path != null && IsChanged)
        {
            return true;
        }
        path = newPath;
        TryLaodFiles();
        return false;
    }
    //  Paths
    public bool SetMapMetaPath(string newPath)
    {
        return CheckPathNotNull(ref MapMetadataFilePath, newPath);
    }
    public bool SetStructurePath(string newPath)
    {
        return CheckPathNotNull(ref MapStructureFilePath, newPath);
    }
    public bool SetCellMetaPath(string newPath)
    {
        return CheckPathNotNull(ref CellMetadataFilePath, newPath);
    }

    //  FUNCTIONS
    //  Loaders
    private void TryLaodFiles()
    {
        if(!string.IsNullOrWhiteSpace(MapMetadataFilePath)
            && !string.IsNullOrWhiteSpace(MapStructureFilePath)
            && !string.IsNullOrWhiteSpace(CellMetadataFilePath))
        {
            MapMetadataFile = new FileInfo(MapMetadataFilePath);
            MapStructureFile = new FileInfo(MapStructureFilePath);
            CellMetadataFile = new FileInfo(CellMetadataFilePath);
        }
    }
    public bool TryLoadData()
    {
        bool areValid = false;
        if(MapMetadataFile != null
            && MapStructureFile != null
            && CellMetadataFile != null)
        {
            areValid = LoadList<Map>(MapMetadataFile, ref Maps) 
                && LoadStructures() 
                && LoadList<BaseObject>(CellMetadataFile, ref MapObjects);
        }
        return areValid;
    }
    public void SaveData()
    {
        if(MapMetadataFile != null
            && MapStructureFile != null
            && CellMetadataFile != null
            && IsChanged)
        {
            SaveList<Map>(ref MapMetadataFile, Maps!);
            SaveStructires();
            SaveList<BaseObject>(ref CellMetadataFile, MapObjects!);
        }
    }
    private bool LoadList<T>(FileInfo file, ref List<T>? values)
    {
        bool isValid = false;
        FileStream stream = file!.OpenRead();
        if(stream.Length == 0)
        {
            values = new List<T>();
            isValid = true;
        }
        else
        {
            StreamReader reader = new StreamReader(stream);
            Container<T> container = JsonSerializer.Deserialize<Container<T>>(reader.ReadToEnd())!;
            if(container.GetVersion() == VERSION)
            {
                values = container.GetValues();
                isValid = true;
            }
            reader.Dispose();
        }
        stream.Dispose();
        return isValid;
    }
    private bool LoadStructures()
    {
        bool isValid = false;
        FileStream stream = MapStructureFile!.OpenRead();
        if(stream.Length == 0)
        {
            MapStructures = new List<byte>();
            isValid = true;
        }
        else
        {
            byte[] v = new byte[2];
            stream.ReadExactly(v);
            if(v[0] * 256 + v[1] == VERSION)
            {
                stream.Seek(2, SeekOrigin.Begin);
                byte[] bytes = new byte[stream.Length - 2];
                stream.ReadExactly(bytes);
                MapStructures = bytes.ToList();
                isValid = true;
            }
        }
        stream.Dispose();
        return isValid;
    }
    //  Savers
    private void SaveList<T>(ref FileInfo file, List<T> values)
    {
        Container<T> container = new Container<T>(VERSION, values.ToArray());
        FileStream stream = file.OpenWrite();
        StreamWriter writer = new StreamWriter(stream);
        string jsonContainer = JsonSerializer.Serialize<Container<T>>(container);
        writer.Write(jsonContainer);
        writer.Dispose(); stream.Dispose();
    }
    private void SaveStructires()
    {
        byte[] bytes = new byte[MapStructures!.Count + 2];
        bytes[0] = VERSION / 256; bytes[1] = VERSION & 0xFF;
        MapStructures.CopyTo(bytes, 2);
        FileStream stream = MapStructureFile!.OpenWrite();
        stream.Write(bytes);
        stream.Dispose();
    }

    //  EVENT SENDERS
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
    //  Object
    public event Action<int>? SelectedObjectChabged;
    protected virtual void OnSelectedObjectChanged(int selectedObject)
    {
        SelectedObjectChabged?.Invoke(selectedObject);
    }
}