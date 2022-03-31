namespace VolumeScroller;

public class MainModel
{
    public MainModel()
    {
        ResetRunOnStartup();
    }

    public int Increment
    {
        get => Properties.Settings.Default.Increment;
        set
        {
            Properties.Settings.Default.Increment = value;
            Properties.Settings.Default.Save();
        }
    }
    public bool RunOnStartup
    {
        get => Properties.Settings.Default.RunOnStartup;
        set
        {
            Properties.Settings.Default.RunOnStartup = value;
            Properties.Settings.Default.Save();
            new StartupManager(Process.GetCurrentProcess()).Set(value);
        }
    }

    public void ResetRunOnStartup() 
        => RunOnStartup = Properties.Settings.Default.RunOnStartup;
}
