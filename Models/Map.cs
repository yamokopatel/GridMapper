public class Map
{
    public byte MapId
    {
        get; private set;
    }
    public byte BiomeId
    {
        get; private set;
    }
    public int ByteOffset
    {
        get; private set;
    }
    public string MapName
    {
        get; private set;
    }

    public Map (byte mapId, byte biomeId, int byteOffset, string mapName)
    {
        this.MapId = mapId;
        this.BiomeId = biomeId;
        this.ByteOffset = byteOffset;
        this.MapName = mapName;
    }

    public byte GetMapId() => MapId;
    public byte GetBiomeId() => BiomeId;
    public int GetByteOffset() => ByteOffset;
    public string GetMapName() => MapName;

    public void SetBiomeId(byte biomeId){this.BiomeId = biomeId;}
    public void SetByteOffset(int byteOffset){this.ByteOffset = byteOffset;}
    public void SetMapName(string mapName){this.MapName = mapName;}
}