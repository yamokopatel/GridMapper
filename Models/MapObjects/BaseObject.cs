public class BaseObject
{
    private readonly ushort ObjectId;
    private readonly byte MapId;
    private readonly byte X;
    private readonly byte Z;

    public BaseObject(ushort objectId, byte mapId, byte x, byte z)
    {
        this.ObjectId = objectId;
        this.MapId = mapId;
        this.X = x;
        this.Z = z;
    }

    public ushort GetObjectId() => ObjectId;
    public byte GetMapId() => MapId;
    public byte GetX() => X;
    public byte GetZ() => Z;
}