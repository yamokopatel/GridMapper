public class Transition : BaseObject
{
    public byte NextMapId
    {
        get; private set;
    }
    public byte NextX
    {
        get; private set;
    }
    public byte NextZ
    {
        get; private set;
    }
    public Direction NextDirection
    {
        get; private set;
    }

    public Transition (ushort objectId, byte mapId, byte x, byte z, byte nextMapId, byte nextX, byte nextZ, Direction nextDirection)
    : base(objectId, mapId, x, z)
    {
        this.NextMapId = nextMapId;
        this.NextX = nextX;
        this.NextZ = nextZ;
        this.NextDirection = nextDirection;
    }
    public Transition (ushort objectId, byte mapId, byte x, byte z)
        : base(objectId, mapId, x, z)
    {
        this.NextMapId = 0;
        this.NextX = 0;
        this.NextZ = 0;
        this.NextDirection = new Direction();
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