public class Transition : BaseObject
{
    private byte NextMapId;
    private byte NextX;
    private byte NextZ;
    private Direction NextDirection;

    public Transition (ushort objectId, byte mapId, byte x, byte z, byte nextMapId, byte nextX, byte nextZ, Direction nextDirection)
    : base(objectId, mapId, x, z)
    {
        this.NextMapId = nextMapId;
        this.NextX = nextX;
        this.NextZ = nextZ;
        this.NextDirection = nextDirection;
    }

    public byte GetNextMapId() => NextMapId;
    public byte GetNextX() => NextX;
    public byte GetNextZ() => NextZ;
    public Direction GetNextDirection() => NextDirection;

    public void SetNextMapId(byte nextMapId){this.NextMapId = nextMapId;}
    public void SetNextX(byte nextX){this.NextX = nextX;}
    public void SetNextZ(byte nextZ){this.NextZ = nextZ;}
    public void SetNextDirection(Direction nextDirection){this.NextDirection = nextDirection;}
}