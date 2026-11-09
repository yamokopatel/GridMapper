using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using GridMapper.ViewModels.Tabs;

namespace GridMapper.Views.Tabs;

public partial class MapEditor : UserControl
{
    #region FIELDS
    private MapEditorViewModel mevm;
    #endregion

    #region CONSTRUCTOR
    public MapEditor()
    {
        mevm = new MapEditorViewModel();
        DataContext = mevm;
        // Initialization
        InitializeComponent();
        // Substription
        mevm.OtherMapSelected += (mapIndex) =>
        {
            OnSelectedMapChanged(mapIndex);
        };
    }
    #endregion
    
    #region EVENTS
    public static readonly RoutedEvent<NumberChangedEventArgs<int>> SelectedMapChangedEvent =
        RoutedEvent.Register<MapEditor, NumberChangedEventArgs<int>>(
            nameof(SelectedMapChanged),
            RoutingStrategies.Bubble
        );
    #endregion

    #region EVENT HANDLERS
    public event EventHandler<NumberChangedEventArgs<int>> SelectedMapChanged
    {
        add => AddHandler(SelectedMapChangedEvent, value);
        remove => RemoveHandler(SelectedMapChangedEvent, value);
    }
    #endregion

    #region EVENT SENDERS
    protected virtual void OnSelectedMapChanged(int mapIndex)
    {
        RaiseEvent(new NumberChangedEventArgs<int>(SelectedMapChangedEvent, mapIndex));
    }
    #endregion
}