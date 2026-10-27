public struct Direction
{
    public sbyte X;
    public sbyte Z;
    public Direction(sbyte xDirection, sbyte zDirection)
    {
        X = xDirection; Z = zDirection;
    }
    public Direction()
    {
        X = 0; Z = 1;
    }
}