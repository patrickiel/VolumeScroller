namespace VolumeScroller;

public class MainModel : IDisposable
{
    private readonly StartupManager startupManager;
    
    private DebugVisualizer debugVisualizer;

    public MainModel()
    {
        startupManager = new StartupManager(Process.GetCurrentProcess());
        startupManager.Set(RunOnStartup);
    }

    public static bool IsDebugMode
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
    
    public void UpdateVisualizer()
    {
        if (IsDebugMode)
        {
            debugVisualizer?.Dispose();
            debugVisualizer = new DebugVisualizer();
        }
        else
        {
            debugVisualizer?.Dispose();
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

    public void Dispose()
    {
        debugVisualizer?.Dispose();
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