public class BaseObject
{
    public ushort ObjectId
    {
        get; private set;
    }
    public byte MapId
    {
        get; private set;
    }
    public byte X
    {
        get; private set;
    }
    public byte Z
    {
        get; private set;
    }

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