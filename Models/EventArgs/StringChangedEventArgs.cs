using Avalonia.Interactivity;

public class StringChangedEventArgs : RoutedEventArgs
{
    public string S { get; }
    public StringChangedEventArgs(RoutedEvent routedEvent, string s)
        : base(routedEvent)
    {
        S = s;
    }
}