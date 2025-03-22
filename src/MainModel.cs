namespace VolumeScroller;

public class MainModel
{
    private readonly StartupManager startupManager;

    public MainModel()
    {
        startupManager = new StartupManager(Process.GetCurrentProcess(), RunOnStartup);
    }

    public bool IsDebugMode
    {
        get => Properties.Settings.Default.IsDebugMode;
        set
        {
            if (Properties.Settings.Default.IsDebugMode != value)
            {
                Properties.Settings.Default.IsDebugMode = value;
                Properties.Settings.Default.Save();
            }
        }
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

    public TriggerMode Mode
    {
        get => (TriggerMode)Properties.Settings.Default.TriggerMode;
        set
        {
            Properties.Settings.Default.TriggerMode = (int)value;
            Properties.Settings.Default.Save();
        }
    }

    public bool EnableTopLeft
    {
        get => Properties.Settings.Default.EnableTopLeft;
        set
        {
            Properties.Settings.Default.EnableTopLeft = value;
            Properties.Settings.Default.Save();
        }
    }

    public bool EnableTopRight
    {
        get => Properties.Settings.Default.EnableTopRight;
        set
        {
            Properties.Settings.Default.EnableTopRight = value;
            Properties.Settings.Default.Save();
        }
    }

    public bool EnableBottomLeft
    {
        get => Properties.Settings.Default.EnableBottomLeft;
        set
        {
            Properties.Settings.Default.EnableBottomLeft = value;
            Properties.Settings.Default.Save();
        }
    }

    public bool EnableBottomRight
    {
        get => Properties.Settings.Default.EnableBottomRight;
        set
        {
            Properties.Settings.Default.EnableBottomRight = value;
            Properties.Settings.Default.Save();
        }
    }

    public int EdgeTolerance
    {
        get => Properties.Settings.Default.EdgeTolerance;
        set
        {
            Properties.Settings.Default.EdgeTolerance = value;
            Properties.Settings.Default.Save();
        }
    }

    public bool EnableCtrlMute
    {
        get => Properties.Settings.Default.EnableCtrlMute;
        set
        {
            Properties.Settings.Default.EnableCtrlMute = value;
            Properties.Settings.Default.Save();
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