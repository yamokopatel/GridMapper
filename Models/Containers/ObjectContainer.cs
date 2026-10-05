using System.Collections.Generic;

public class ObjectContainer
{
    private readonly uint version;
    private BaseObject[] objects;

    public ObjectContainer(uint version, BaseObject[] objects)
    {
        this.version = version; this.objects = objects;
    }

    public uint GetVersion() => version;
    public List<BaseObject> GetObjects() => new List<BaseObject>(objects);

    public void SetObjects(List<BaseObject> objects){this.objects = objects.ToArray();}
}