using System.Collections.Generic;

public class Chest : BaseObject
{
    private List<byte> SubjectIds;

    public Chest(ushort objectId, byte mapId, byte x, byte z, byte[] subjects) : base(objectId, mapId, x, z)
    {
        this.SubjectIds = new List<byte>(subjects);
    }

    public List<byte> GetSubjectIds() => new List<byte>(SubjectIds);
    public void SetSubjectIds(List<byte> subjectIds)
    {
        this.SubjectIds = new List<byte>(subjectIds);
    }
}