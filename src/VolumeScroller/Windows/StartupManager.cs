namespace VolumeScroller;

internal class StartupManager(Process process)
{
    private readonly Process process = process;

    public void Set(bool runOnStartup)
    {
        string filePath = process.MainModule.FileName;

        RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        if (runOnStartup)
        {
            key.SetValue(process.ProcessName, $"\"{filePath}\"");
        }
        else
        {
            key.DeleteValue(process.ProcessName, false);
        }
    }
}
