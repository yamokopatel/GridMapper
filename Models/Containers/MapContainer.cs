using System.Collections.Generic;

public class MapContainer
{
    private readonly uint version;
    private Map[] maps;

    public MapContainer(uint version, Map[] maps)
    {
        this.version = version; this.maps = maps;
    }

    public uint GetVersion() => version;
    public List<Map> GetMaps() => new List<Map>(maps);

    public void SetMaps(List<Map> maps){this.maps = maps.ToArray();}
}