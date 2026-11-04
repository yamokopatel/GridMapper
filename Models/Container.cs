using System.Collections.Generic;

public class Container<T>
{
    public ushort version
    {
        get;
        private set;
    }
    public T[] values
    {
        get;
        set;
    }

    public Container(ushort version, T[] values)
    {
        this.version = version; this.values = values;
    }

    public ushort GetVersion() => version;
    public List<T> GetValues() => new List<T>(values);

    public void SetValues(List<T> values){this.values = values.ToArray();}
}