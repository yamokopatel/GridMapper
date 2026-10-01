using System;

// Wanted to use, but this struct is useless.
// I leave it here to not lose progress...(((
public struct DynamicArray <T>
{
    private T[] Main;

    //  CONSTUCTORS
    // empty
    public DynamicArray(){
        this.Main = new T[0];
    }
    //  filled
    public DynamicArray(T[] values)
    {
        this.Main = values;
    }

    //  ADDERS
    //  single
    public void Add(T value)
    {
        int ML = Main.Length;
        T[] Arr = new T[ML + 1];
        for(int a = 0; a < ML; a++)
        {
            Arr[a] = Main[a];
        }
        Arr[ML] = value;
        Main = Arr;
    }
    //  multiple
    public void Add(T[] values)
    {
        int ML = Main.Length;
        int L = ML + values.Length;
        T[] Arr = new T[L];
        for(int a = 0; a < L; a++)
        {
            if(a < ML)
            {
                Arr[a] = Main[a];
            }
            else
            {
                Arr[a] = values[a - ML];
            }
        }
        Main = Arr;
    }

    //  GETTERS
    //  size
    public int Size() => Main.Length;
    //  single
    public T Get(int index) => Main[index];
    //  multiple
    public T[] Get(int firstIndex, int lastIndex)
    {
        T[] Arr = new T[lastIndex - firstIndex + 1];
        for(int a = firstIndex; a <= lastIndex; a++)
        {
            Arr[a - firstIndex] = Main[a];
        }
        return Arr;
    }

    //  SETTERS
    //  single
    public void Set(int index, T value)
    {
        Main[index] = value;
    }
    //  multiple
    public void Set(int firstIndex, int lastIndex, T[] values)
    {
        int NL = values.Length;
        if(NL == (lastIndex - firstIndex + 1))
        {
            for(int a = 0; a < NL; a++)
            {
                Main[a + firstIndex] = values[a];
            }
        }
    }

    //  REMOVERS
    //  single
    public void Remove(int index)
    {
        T[] Arr = new T[Main.Length - 1];
        for(int a = 0; a < Main.Length; a++)
        {
            if(a < index)
            {
                Arr[a] = Main[a];
            }
            else if(a > index)
            {
                Arr[a - 1] = Main[a];
            }
        }
        Main = Arr;
    }
    //  multiple
    public void Remove(int firstIndex, int lastIndex)
    {
        T[] Arr = new T[Main.Length - (lastIndex - firstIndex + 1)];
        for(int a = 0; a < Main.Length; a++)
        {
            if(a < firstIndex)
            {
                Arr[a] = Main[a];
            }
            else if (a > lastIndex)
            {
                Arr[a - (lastIndex - firstIndex + 1)] = Main[a];
            }
        }
        Main = Arr;
    }
}