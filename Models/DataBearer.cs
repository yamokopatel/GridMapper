using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public class DataBearer
{
    private const ushort VERSION = 0x7D_01;
    //  PARAMETERS
    private bool IsChanged = false;
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
    public void SetChanged(bool changed){IsChanged = changed;}
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
        bool result = false;
        if(MapMetadataFile != null
            && MapStructureFile != null
            && CellMetadataFile != null)
        {
            result = LoadMaps() && LoadStructures() && LoadCells();
        }
        return result;
    }
    private bool LoadMaps()
    {
        bool result = false;
        FileStream stream = MapMetadataFile!.OpenRead();
        if(stream.Length == 0)
        {
            Maps = new List<Map>();
            result = true;
        }
        else
        {
            Container<Map> maps = JsonSerializer.Deserialize<Container<Map>>(stream)!;
            if (maps.GetVersion().Equals(VERSION))
            {
                Maps = maps.GetValues();
                result = true;
            }
        }
        stream.Dispose();
        return result;
    }
    private bool LoadCells()
    {
        bool result = false;
        FileStream stream = CellMetadataFile!.OpenRead();
        if(stream.Length == 0)
        {
            MapObjects = new List<BaseObject>();
            result = true;
        }
        else
        {
            Container<BaseObject> cells = JsonSerializer.Deserialize<Container<BaseObject>>(stream)!;
            if (cells.GetVersion().Equals(VERSION))
            {
                MapObjects = cells.GetValues();
                result = true;
            }
        }
        stream.Dispose();
        return result;
    }
    private bool LoadStructures()
    {
        bool result = false;
        FileStream stream = MapStructureFile!.OpenRead();
        if(stream.Length == 0)
        {
            MapStructures = new List<byte>();
            result = true;
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
                result = true;
            }
        }
        stream.Dispose();
        return result;
    }
}