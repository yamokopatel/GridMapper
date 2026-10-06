using System.Collections.Generic;

public class Container<T>
{
    private readonly uint version;
    private T[] values;

    public Container(uint version, T[] values)
    {
        this.version = version; this.values = values;
    }

    public uint GetVersion() => version;
    public List<T> GetValues() => new List<T>(values);

    public void SetValues(List<T> values){this.values = values.ToArray();}
}