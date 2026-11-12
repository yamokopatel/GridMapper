using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using GridMapper.ViewModels.Tabs;

namespace GridMapper.Views.Tabs;

public partial class StructureEditor : UserControl
{
    #region FIELDS
    private StructureEditorViewModel sevm;
    #endregion

    #region CONSTRUCTOR
    public StructureEditor()
    {
        sevm = new StructureEditorViewModel();
        DataContext = sevm;
        // Initialization
        InitializeComponent();
        #region Subscriptions
        #region native
        sevm.OtherMapSelected += (mapIndex) =>
        {
            OnSelectedMapChanged(mapIndex);
        };
        #endregion

        #region avalonia
        this.AddHandler(MainWindow.MapListLoadedEvent, OnMapListLoaded);
        this.AddHandler(MainWindow.ProcessableMapChangedEvent, OnProcessableMapChanged);
        this.AddHandler(MainWindow.MapListEditedEvent, OnMapListEdited);
        this.AddHandler(MainWindow.StructureListGotEvent, OnStructuresGot);
        this.AddHandler(MainWindow.ObjectListGotEvent, OnObjectsGot);
        #endregion
        #endregion
    }
    #endregion

    #region FUNCTIONS
    private void OnMapListLoaded(object? sender, ListEventAgrs<Map> args)
    {
        sevm.SetMapList(args.Values);
    }
    private void OnProcessableMapChanged(object? sender, NumberChangedEventArgs<int> args)
    {
        sevm.SetSelectedMap(args.Value);
    }
    private void OnMapListEdited(object? sender)
    {
        sevm.UpdateSelector();
    }
    private void OnStructuresGot(object? sender, ListEventAgrs<byte> agrs)
    {
        sevm.SetStructureList(agrs.Values);
    }
    private void OnObjectsGot(object? sender, ListEventAgrs<BaseObject> agrs)
    {
        sevm.SetObjectList(agrs.Values);
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