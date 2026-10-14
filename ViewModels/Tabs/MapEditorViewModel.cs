using System.Collections.Generic;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace GridMapper.ViewModels.Tabs;

public partial class MapEditorViewModel : ViewModelBase
{
    public DataBearer? BearerIn;

    //  PROPERTIES
    [ObservableProperty]
    public partial List<ComboBoxItem> MapOptions { get; set; } = new List<ComboBoxItem>();
    [ObservableProperty]
    public partial bool SettingsVisible { get; set; } = false;
}