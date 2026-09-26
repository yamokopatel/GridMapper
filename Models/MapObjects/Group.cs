using System.Collections.Generic;

public class Group : BaseObject
{
    private List<byte> EnemyIds;

    public Group(ushort objectId, byte mapId, byte x, byte z, byte[] enemyIds)
    : base(objectId, mapId, x, z)
    {
        this.EnemyIds = new List<byte>(enemyIds);
    }

    public List<byte> GetEnemyIds() => new List<byte>(EnemyIds);
    public void SetEnemyIds(List<byte> enemyIds){this.EnemyIds = new List<byte>(enemyIds);}
}