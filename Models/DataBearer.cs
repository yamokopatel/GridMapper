using System.Collections.Generic;
using System.IO;

public class DataBearer
{
    //  PARAMETERS
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
    //  Paths
    public string? GetMapMetadataPath() => MapMetadataFilePath;
    public string? GetMapStructurePath() => MapStructureFilePath;
    public string? GetCellMetadataPath() => CellMetadataFilePath;
    //  Data
    public List<Map>? GetMaps() => Maps;
    public List<byte>? GetStructures() => MapStructures;
    public List<BaseObject>? GetMapObjects() => MapObjects;

    //  SETTERS
    //  Checker, will be necessary later
    private bool CheckPathNotNull(string? path)
    {
        if(path != null)
        {
            return true;
        }
        return false;
    }
    //  Paths
    public void SetMapMetaPath(string newPath)
    {
        MapMetadataFilePath = newPath;
    }
    public void SetStructurePath(string newPath)
    {
        MapStructureFilePath = newPath;
    }
    public void SetCellMetaPath(string newPath)
    {
        CellMetadataFilePath = newPath;
    }
}