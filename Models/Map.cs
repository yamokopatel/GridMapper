public class Map
{
    private readonly byte MapId;
    private readonly byte BiomeId;

    public Map (byte mapId, byte biomeId)
    {
        this.MapId = mapId;
        this.BiomeId = biomeId;
    }

    public byte GetMapId() => MapId;
    public byte GetBiomeId() => BiomeId;
}