using CommunityToolkit.Mvvm.ComponentModel;

namespace GridMapper.ViewModels.Components;

public partial class NoticeWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    public partial string NotificationText { get; set; } = "Notification text placeholder";

    public NoticeWindowViewModel(string notificationText)
    {
        NotificationText = notificationText;
    }
}