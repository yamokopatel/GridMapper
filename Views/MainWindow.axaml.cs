using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using GridMapper.ViewModels;
using GridMapper.Views.Tabs;

namespace GridMapper.Views;

public partial class MainWindow : Window
{
    #region FIELDS
    private MainViewModel mvm;
    public IBrush SaveUndoColor
    {
        get => GetValue(SaveUndoColorProperty);
        set => SetValue(SaveUndoColorProperty, value);
    }
    #endregion
    
    #region PROPERTIES
    public static readonly StyledProperty<IBrush> SaveUndoColorProperty = 
        AvaloniaProperty.Register<MainWindow, IBrush>(nameof(SaveUndoColor));
    #endregion

    #region CONSTRUCTOR
    public MainWindow()
    {
        mvm = new MainViewModel();
        DataContext = mvm;
        // Initialization
        SaveUndoColor = Brushes.DarkGray;
        InitializeComponent();
        #region Subscriptions
        #region native
        mvm.DataChanged += (changed) =>
        {
            SetSaveUndoColor(changed);
        };
        #region _map
        mvm.MapsLoaded += (maps) =>
        {
            OnMapListLoaded(maps);
        };
        mvm.SelectedMapChanged += (mapIndex) =>
        {
            OnProcessableMapChanged(mapIndex);
        };
        mvm.MapCreatedOrDeleted += () =>
        {
            OnMapCreatedOrDeleted();
            OnMapListEdited();
        };
        #endregion
        #region _structure
        mvm.StructuresGot += (structures) =>
        {
            OnStructuresGot(structures);
        };
        #endregion
        #region _objects
        mvm.ObjectsGot += (objects) =>
        {
            OnObjectsGot(objects);
        };
        #endregion
        #endregion

        #region avalonia
        // MAP SELECTED
        this.AddHandler(MapEditor.SelectedMapChangedEvent, OnSelectedMapChanged);
        this.AddHandler(StructureEditor.SelectedMapChangedEvent, OnSelectedMapChanged);
        this.AddHandler(ObjectEditor.SelectedMapChangedEvent, OnSelectedMapChanged);
        // PATHS
        this.AddHandler(FileBrowser.MapMetaPathChangedEvent, OnMapMetaPathChanged);
        this.AddHandler(FileBrowser.StructurePathChangedEvent, OnStructurePathChanged);
        this.AddHandler(FileBrowser.CellMetaPathChangedEvent, OnCellMetaPathChanged);
        // DATA CHANGED
        this.AddHandler(MapEditor.MapListEditedEvent, OnMapsEdited);
        this.AddHandler(MapEditor.MapCreatedEvent, OnMapCreated);
        this.AddHandler(MapEditor.MapDeletedEvent, OnMapDeleted);
        #endregion
        #endregion
    }
    #endregion

    #region FUNCTIONS
    private void SetSaveUndoColor(bool changed)
    {
        SaveUndoColor = (changed ? Brushes.LightGray : Brushes.DarkGray);
    }
    private void OnSelectedMapChanged(object? sender, NumberChangedEventArgs<int> args)
    {
        mvm.SetProcessingMap(args.Value);
    }
    private void OnMapMetaPathChanged(object? sender, StringChangedEventArgs args)
    {
        mvm.SetMapMetaPath(args.S);
    }
    private void OnStructurePathChanged(object? sender, StringChangedEventArgs args)
    {
        mvm.SetStructurePath(args.S);
    }
    private void OnCellMetaPathChanged(object? sender, StringChangedEventArgs args)
    {
        mvm.SetCellMetaPath(args.S);
    }
    private void OnMapsEdited(object? sender)
    {
        mvm.SetChanged(true);
        OnMapListEdited();
    }
    private void OnMapCreated(object? sender)
    {
        mvm.CreateMap();
    }
    private void OnMapDeleted(object? sender)
    {
        mvm.DeleteMap();
    }
    #endregion

    #region EVENTS
    public static readonly RoutedEvent<NumberChangedEventArgs<int>> ProcessableMapChangedEvent =
        RoutedEvent.Register<MainWindow, NumberChangedEventArgs<int>>(
            nameof(ProcessableMapChanged),
            RoutingStrategies.Tunnel
        );
    public static readonly RoutedEvent<ListEventAgrs<Map>> MapListLoadedEvent =
        RoutedEvent.Register<MainWindow, ListEventAgrs<Map>>(
            nameof(MapListLoaded),
            RoutingStrategies.Tunnel
        );
    public static readonly RoutedEvent<RoutedEventArgs> MapListEditedEvent =
        RoutedEvent.Register<MainWindow, RoutedEventArgs>(
            nameof(MapListEdited),
            RoutingStrategies.Tunnel
        );
    public static readonly RoutedEvent<RoutedEventArgs> MapCreatedOrDeletedEvent =
        RoutedEvent.Register<MainWindow, RoutedEventArgs>(
            nameof(MapCreatedOrDeleted),
            RoutingStrategies.Tunnel
        );
    public static readonly RoutedEvent<ListEventAgrs<byte>> StructureListGotEvent =
        RoutedEvent.Register<MainWindow, ListEventAgrs<byte>>(
            nameof(StructureListGot),
            RoutingStrategies.Tunnel
        );
    public static readonly RoutedEvent<ListEventAgrs<BaseObject>> ObjectListGotEvent =
        RoutedEvent.Register<MainWindow, ListEventAgrs<BaseObject>>(
            nameof(ObjectListGot),
            RoutingStrategies.Tunnel
        );
    #endregion

    #region EVENT HANDLERS
    public event EventHandler<NumberChangedEventArgs<int>> ProcessableMapChanged
    {
        add => AddHandler(ProcessableMapChangedEvent, value);
        remove => RemoveHandler(ProcessableMapChangedEvent, value);
    }
    public event EventHandler<ListEventAgrs<Map>> MapListLoaded
    {
        add => AddHandler(MapListLoadedEvent, value);
        remove => RemoveHandler(MapListLoadedEvent, value);
    }
    public event EventHandler<RoutedEventArgs> MapListEdited
    {
        add => AddHandler(MapListEditedEvent, value);
        remove => RemoveHandler(MapListEditedEvent, value);
    }
    public event EventHandler<RoutedEventArgs> MapCreatedOrDeleted
    {
        add => AddHandler(MapCreatedOrDeletedEvent, value);
        remove => RemoveHandler(MapCreatedOrDeletedEvent, value);
    }
    public event EventHandler<ListEventAgrs<byte>> StructureListGot
    {
        add => AddHandler(StructureListGotEvent, value);
        remove => RemoveHandler(StructureListGotEvent, value);
    }
    public event EventHandler<ListEventAgrs<BaseObject>> ObjectListGot
    {
        add => AddHandler(ObjectListGotEvent, value);
        remove => RemoveHandler(ObjectListGotEvent, value);
    }
    #endregion

    #region EVENT SENDERS
    protected virtual void OnProcessableMapChanged(int mapIndex)
    {
        RaiseEvent(new NumberChangedEventArgs<int>(ProcessableMapChangedEvent, mapIndex));
    }
    protected virtual void OnMapListLoaded(List<Map> maps)
    {
        RaiseEvent(new ListEventAgrs<Map>(MapListLoadedEvent, maps));
    }
    protected virtual void OnMapListEdited()
    {
        RaiseEvent(new RoutedEventArgs(MapListEditedEvent));
    }
    protected virtual void OnMapCreatedOrDeleted()
    {
        RaiseEvent(new RoutedEventArgs(MapCreatedOrDeletedEvent));
    }
    protected virtual void OnStructuresGot(List<byte> structures)
    {
        RaiseEvent(new ListEventAgrs<byte>(StructureListGotEvent, structures));
    }
    protected virtual void OnObjectsGot(List<BaseObject> objects)
    {
        RaiseEvent(new ListEventAgrs<BaseObject>(ObjectListGotEvent, objects));
    }
    #endregion
}