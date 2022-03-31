using Microsoft.Win32;

namespace VolumeScroller;

public class MainViewModel : ViewModelBase, IDisposable
{
    private readonly MainModel mainModel;
    private int increment;
    private bool runOnStartup;
    private string taskBarIconPath;

    public MainViewModel(MainModel mainModel)
    {
        this.mainModel = mainModel;
        increment = mainModel.Increment * 2;
        runOnStartup = mainModel.RunOnStartup;
        TaskBarIconPath = GetTaskbarIconPath();
        SystemEvents.UserPreferenceChanged += UserPreferenceChanged;
    }

    public string TaskBarIconPath
    {
        get => taskBarIconPath;
        set => SetProperty(ref taskBarIconPath, value);
    }

    public bool RunOnStartup
    {
        get => runOnStartup;
        set
        {
            SetProperty(ref runOnStartup, value);
            mainModel.RunOnStartup = value;
        }
    }

    public int Increment
    {
        get => increment;
        set
        {
            SetProperty(ref increment, value);
            mainModel.Increment = value / 2;
        }
    }

    public void Dispose() 
        => SystemEvents.UserPreferenceChanged -= UserPreferenceChanged;

    private void UserPreferenceChanged(object sender, EventArgs e) 
        => TaskBarIconPath = GetTaskbarIconPath();

    private static string GetTaskbarIconPath()
    {
        RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
        var isLightTheme = (int)key.GetValue("SystemUsesLightTheme") == 1;

        return isLightTheme
            ? "/Resources/VolumeScroller_light.ico"
            : "/Resources/VolumeScroller_dark.ico";
    }
}
