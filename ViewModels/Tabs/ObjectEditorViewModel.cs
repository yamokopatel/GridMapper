using CommunityToolkit.Mvvm.ComponentModel;

namespace GridMapper.ViewModels.Tabs;

public partial class ObjectEditorViewModel : ViewModelBase
{
    private DataBearer BearerIn;

    #region PROPERTIES
    //  CHOSES
    //  Object Type
    [ObservableProperty]
    public partial int SelectedObjectType { get; set; } = 0;
    partial void OnSelectedObjectTypeChanged(int value)
    {
        ChestChosen = (value == 1);
        GroupChosen = (value == 2);
        TransitionChosen = (value ==3);
    }
    //  PARAMETERS
    //  Chest
    [ObservableProperty]
    public partial int SelectedSubject { get; set; } = -1;
    [ObservableProperty]
    public partial decimal? SubjectId { get; set; } = null;
    //  Group
    [ObservableProperty]
    public partial int SelectedEnemy { get; set; } = -1;
    [ObservableProperty]
    public partial decimal? EnemyId { get; set; } = null;
    //  Transition
    //  map
    [ObservableProperty]
    public partial int SelectedNextMap { get; set; } = -1;
    partial void OnSelectedNextMapChanged(int value)
    {
        TransitionParametersVisible = (value > -1);
    }
    //  x
    [ObservableProperty]
    public partial decimal? NextXValue { get; set; } = null;
    //  y
    [ObservableProperty]
    public partial decimal? NextYValue { get; set; } = null;
    //  x direction
    [ObservableProperty]
    public partial decimal? NextXDirValue { get; set; } = null;
    partial void OnNextXDirValueChanged(decimal? value)
    {
        if(NextYDirValue != 0)
        {
            NextYDirValue = 0;
        }
    }
    //  y direction
    [ObservableProperty]
    public partial decimal? NextYDirValue { get; set; } = null;
    partial void OnNextYDirValueChanged(decimal? value)
    {
        if(NextXDirValue != 0)
        {
            NextXDirValue = 0;
        }
    }
    //  VISIBILITY
    //  Object
    //  chest
    [ObservableProperty]
    public partial bool ChestChosen { get; set; } = false;
    //  group
    [ObservableProperty]
    public partial bool GroupChosen { get; set; } = false;
    //  transition
    [ObservableProperty]
    public partial bool TransitionChosen { get; set; } = false;
    //  transition parameters
    [ObservableProperty]
    public partial bool TransitionParametersVisible { get; set; } = false;
    #endregion

    public ObjectEditorViewModel(DataBearer bearerIn)
    {
        BearerIn = bearerIn;
    }
}