using System;
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
        mvm.SelectedMapChanged += (mapIndex) =>
        {
            OnProcessableMapChanged(mapIndex);
        };
        // BearerOut.DataChanged += (changed) =>
        // {
        //     SetSaveUndoColor(changed);
        // };
        #endregion

        #region avalonia
        this.AddHandler(MapEditor.SelectedMapChangedEvent, OnSelectedMapChanged);
        this.AddHandler(StructureEditor.SelectedMapChangedEvent, OnSelectedMapChanged);
        this.AddHandler(FileBrowser.MapMetaPathChangedEvent, OnMapMetaPathChanged);
        this.AddHandler(FileBrowser.StructurePathChangedEvent, OnStructurePathChanged);
        this.AddHandler(FileBrowser.CellMetaPathChangedEvent, OnCellMetaPathChanged);
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
    #endregion

    #region EVENTS
    public static readonly RoutedEvent<NumberChangedEventArgs<int>> ProcessableMapChangedEvent =
        RoutedEvent.Register<MainWindow, NumberChangedEventArgs<int>>(
            nameof(ProcessableMapChanged),
            RoutingStrategies.Tunnel
        );
    #endregion

    #region EVENT HANDLERS
    public event EventHandler<NumberChangedEventArgs<int>> ProcessableMapChanged
    {
        add => AddHandler(ProcessableMapChangedEvent, value);
        remove => RemoveHandler(ProcessableMapChangedEvent, value);
    }
    #endregion

    #region EVENT SENDERS
    protected virtual void OnProcessableMapChanged(int mapIndex)
    {
        RaiseEvent(new NumberChangedEventArgs<int>(ProcessableMapChangedEvent, mapIndex));
    }
    #endregion
}