using System.Collections.Generic;
using System.IO;

public class DataBearer
{
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
    //  Checker
    private bool CheckPathNotNull(string? path)
    {
        return path != null && IsChanged;
    }
    //  Paths
    public bool SetMapMetaPath(string newPath)
    {
        if (!CheckPathNotNull(MapMetadataFilePath))
        {
            MapMetadataFilePath = newPath;
            TryLaodFiles();
            return false;
        }
        return true;
    }
    public bool SetStructurePath(string newPath)
    {
        if (!CheckPathNotNull(MapStructureFilePath))
        {
            MapStructureFilePath = newPath;
            TryLaodFiles();
            return false;
        }
        return true;
    }
    public bool SetCellMetaPath(string newPath)
    {
        if (!CheckPathNotNull(CellMetadataFilePath))
        {
            CellMetadataFilePath = newPath;
            TryLaodFiles();
            return false;
        }
        return true;
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
}