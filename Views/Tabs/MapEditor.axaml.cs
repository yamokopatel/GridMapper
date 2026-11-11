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
        #region Subscriptions
        #region native
        mevm.OtherMapSelected += (mapIndex) =>
        {
            OnSelectedMapChanged(mapIndex);
        };
        mevm.MapsChanged += () =>
        {
            OnMapListEdited();
        };
        #endregion

        #region avalonia
        this.AddHandler(MainWindow.MapListLoadedEvent, OnMapListLoaded);
        this.AddHandler(MainWindow.ProcessableMapChangedEvent, OnProcessableMapChanged);
        #endregion
        #endregion
    }
    #endregion

    #region FUNCTIONS
    private void OnMapListLoaded(object? sender, ListEventAgrs<Map> args)
    {
        mevm.SetMapList(args.Values);
    }
    private void OnProcessableMapChanged(object? sender, NumberChangedEventArgs<int> args)
    {
        mevm.SetSelectedMap(args.Value);
    }
    #endregion
    
    #region EVENTS
    public static readonly RoutedEvent<NumberChangedEventArgs<int>> SelectedMapChangedEvent =
        RoutedEvent.Register<MapEditor, NumberChangedEventArgs<int>>(
            nameof(SelectedMapChanged),
            RoutingStrategies.Bubble
        );
    public static readonly RoutedEvent<RoutedEventArgs> MapListEditedEvent =
        RoutedEvent.Register<MapEditor, RoutedEventArgs>(
            nameof(MapListEdited),
            RoutingStrategies.Bubble
        );
    #endregion

    #region EVENT HANDLERS
    public event EventHandler<NumberChangedEventArgs<int>> SelectedMapChanged
    {
        add => AddHandler(SelectedMapChangedEvent, value);
        remove => RemoveHandler(SelectedMapChangedEvent, value);
    }
    public event EventHandler<RoutedEventArgs> MapListEdited
    {
        add => AddHandler(MapListEditedEvent, value);
        remove => RemoveHandler(MapListEditedEvent, value);
    }
    #endregion

    #region EVENT SENDERS
    protected virtual void OnSelectedMapChanged(int mapIndex)
    {
        RaiseEvent(new NumberChangedEventArgs<int>(SelectedMapChangedEvent, mapIndex));
    }
    protected virtual void OnMapListEdited()
    {
        RaiseEvent(new RoutedEventArgs(MapListEditedEvent));
    }
    #endregion
}