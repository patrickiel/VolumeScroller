using System.Diagnostics;

namespace VolumeScroller;

internal class StartupManager
{
    private readonly Process process;

    public StartupManager(Process process, bool runOnStartup)
    {
        this.process = process;
        Set(runOnStartup);
    }

    public void Set(bool runOnStartup)
    {
        string filePath = process.MainModule.FileName;

        RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

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
