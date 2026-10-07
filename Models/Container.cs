using System.Collections.Generic;

public class Container<T>
{
    private readonly ushort version;
    private T[] values;

    public Container(ushort version, T[] values)
    {
        this.version = version; this.values = values;
    }

    public ushort GetVersion() => version;
    public List<T> GetValues() => new List<T>(values);

    public void SetValues(List<T> values){this.values = values.ToArray();}
}