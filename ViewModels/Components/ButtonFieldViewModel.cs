namespace GridMapper.ViewModels.Components;

public partial class ButtonFieldViewModel : ViewModelBase
{
    private DataBearer BearerIn;

    public ButtonFieldViewModel(DataBearer bearerIn)
    {
        BearerIn = bearerIn;
    }
}