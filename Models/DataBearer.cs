using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

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
    //  Checker, will be necessary later
    private async Task<bool> CheckPathNotNull(string? path)
    {
        if(path != null)
        {
            if (IsChanged)
            {
                return true;
            }
            return false;
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