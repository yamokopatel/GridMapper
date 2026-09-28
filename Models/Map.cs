public class Map
{
    private readonly byte MapId;
    private byte BiomeId;
    private int ByteOffset;

    public Map (byte mapId, byte biomeId, int byteOffset)
    {
        this.MapId = mapId;
        this.BiomeId = biomeId;
        this.ByteOffset = byteOffset;
    }

    public byte GetMapId() => MapId;
    public byte GetBiomeId() => BiomeId;
    public int GetByteOffset() => ByteOffset;

    public void SetBiomeId(byte biomeId){this.BiomeId = biomeId;}
    public void SetByteOffset(int byteOffset){this.ByteOffset = byteOffset;}
}