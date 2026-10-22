namespace GridMapper.ViewModels.Tabs;

public partial class ObjectEditorViewModel : ViewModelBase
{
    private DataBearer BearerIn;

    public ObjectEditorViewModel(DataBearer bearerIn)
    {
        BearerIn = bearerIn;
    }
}