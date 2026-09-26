public class Transition : BaseObject
{
    private byte NextMapId;
    private byte NextX;
    private byte NextZ;
    private byte[] NextDirection; // should be 2 values

    public Transition (ushort objectId, byte mapId, byte x, byte z, byte nextMapId, byte nextX, byte nextZ, byte[] nextDirection)
    : base(objectId, mapId, x, z)
    {
        this.NextMapId = nextMapId;
        this.NextX = nextX;
        this.NextZ = nextZ;
        this.NextDirection = (byte[])nextDirection.Clone();
    }

    public byte GetNextMapId() => NextMapId;
    public byte GetNextX() => NextX;
    public byte GetNextZ() => NextZ;
    public byte[] GetNextDirection() => (byte[])NextDirection.Clone();

    public void SetNextMapId(byte nextMapId){this.NextMapId = nextMapId;}
    public void SetNextX(byte nextX){this.NextX = nextX;}
    public void SetNextZ(byte nextZ){this.NextZ = nextZ;}
    public void SetNextDirection(byte[] nextDirection){this.NextDirection = (byte[])nextDirection.Clone();}
}