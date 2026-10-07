using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

public class DataBearer
{
    private const ushort version = 0x7D_01;
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
    public void TryLoadData()
    {
        if(MapMetadataFile != null
            && MapStructureFile != null
            && CellMetadataFile != null)
        {
            FileStream stream = MapMetadataFile.OpenRead();
            Maps = JsonSerializer.Deserialize<Container<Map>>(stream)!.GetValues();
            stream = MapStructureFile.OpenRead();
            stream.Seek(2, SeekOrigin.Begin);
            byte[] bytes = new byte[stream.Length - 2];
            stream.ReadExactly(bytes);
            MapStructures = bytes.ToList();
            stream = CellMetadataFile.OpenRead();
            MapObjects = JsonSerializer.Deserialize<Container<BaseObject>>(stream)!.GetValues();
            stream.Dispose();
        }
    }
}