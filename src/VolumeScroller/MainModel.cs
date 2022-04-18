namespace VolumeScroller;

public class MainModel
{
    readonly StartupManager startupManager;

    public MainModel()
    {
        startupManager = new StartupManager(Process.GetCurrentProcess());
        startupManager.Set(RunOnStartup);
    }

    public static int Increment
    {
        get => Properties.Settings.Default.Increment;
        set
        {
            Properties.Settings.Default.Increment = value;
            Properties.Settings.Default.Save();
        }
    }

    public bool TaskbarMustBeVisible
    {
        get => Properties.Settings.Default.TaskbarMustBeVisible;
        set
        {
            Properties.Settings.Default.TaskbarMustBeVisible = value;
            Properties.Settings.Default.Save();
            startupManager.Set(value);
        }
    }

    public static string TaskBarIconPath => GetTaskbarIconPath();

    private static string GetTaskbarIconPath()
    {
        RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize");
        var isLightTheme = (int)key.GetValue("SystemUsesLightTheme") == 1;

        return isLightTheme
            ? "/Resources/VolumeScroller_light.ico"
            : "/Resources/VolumeScroller_dark.ico";
    }

    public bool RunOnStartup
    {
        get => Properties.Settings.Default.RunOnStartup;
        set
        {
            Properties.Settings.Default.RunOnStartup = value;
            Properties.Settings.Default.Save();
            startupManager.Set(value);
        }
    }
}
