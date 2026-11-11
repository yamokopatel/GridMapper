using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GridMapper.ViewModels.Tabs;

namespace GridMapper.Views.Tabs;

public partial class ObjectEditor : UserControl
{
    private ObjectEditorViewModel oevm;
    public ObjectEditor()
    {
        oevm = new ObjectEditorViewModel();
        DataContext = oevm;
        // Initialization
        InitializeComponent();
        #region Subscriptions
        #region native
        oevm.OtherMapSelected += (mapIndex) =>
        {
            OnSelectedMapChanged(mapIndex);
        };
        #endregion

        #region avalonia
        this.AddHandler(MainWindow.ProcessableMapChangedEvent, OnProcessableMapChanged);
        #endregion
        #endregion
    }

    #region FUNCTIONS
    private void OnProcessableMapChanged(object? sender, NumberChangedEventArgs<int> e)
    {
        oevm.SetSelectedMap(e.Value);
    }
    #endregion

    #region EVENTS
    public static readonly RoutedEvent<NumberChangedEventArgs<int>> SelectedMapChangedEvent =
        RoutedEvent.Register<ObjectEditor, NumberChangedEventArgs<int>>(
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