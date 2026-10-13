public class Map
{
    private readonly byte MapId;
    private byte BiomeId;
    private int ByteOffset;
    private string MapName;

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