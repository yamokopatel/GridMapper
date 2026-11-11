using System.Collections.Generic;
using Avalonia.Interactivity;

public class ListEventAgrs<T> : RoutedEventArgs
{
    public List<T> Values { get; }
    public ListEventAgrs(RoutedEvent routedEvent, List<T> values)
        : base(routedEvent)
    {
        Values = values;
    }
}