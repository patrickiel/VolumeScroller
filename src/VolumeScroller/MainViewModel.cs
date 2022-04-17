using Microsoft.Win32;

namespace VolumeScroller;

public class MainViewModel : ViewModelBase, IDisposable
{
    private string taskBarIconPath;

    public MainViewModel(MainModel mainModel)
    {     
        SystemEvents.UserPreferenceChanged += UserPreferenceChanged;
    }

    public string TaskBarIconPath => MainModel.TaskBarIconPath;

    public static bool RunOnStartup
    {
        get => MainModel.RunOnStartup;
        set => MainModel.RunOnStartup = value;
    }

    public static bool TaskbarMustBeVisible
    {
        get => MainModel.TaskbarMustBeVisible;
        set => MainModel.TaskbarMustBeVisible = value;
    }

    public static int Increment
    {
        get => MainModel.Increment * 2;
        set => MainModel.Increment = value / 2;
    }

    public void Dispose()
        => SystemEvents.UserPreferenceChanged -= UserPreferenceChanged;

    private void UserPreferenceChanged(object sender, EventArgs e) 
        => OnPropertyChanged(nameof(TaskBarIconPath));
}
