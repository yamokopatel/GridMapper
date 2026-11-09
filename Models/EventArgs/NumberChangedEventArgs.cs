using System.Numerics;
using Avalonia.Interactivity;

public class NumberChangedEventArgs<T> : RoutedEventArgs
    where T : INumber<T>
{
    public T Value { get; }

    public NumberChangedEventArgs(RoutedEvent routedEvent, T value)
        : base(routedEvent)
    {
        Value = value;
    }
}