using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace VolumeScroller;

internal class StartupManager
{
    private readonly Process process;

    public StartupManager(Process process)
    {
        this.process = process;
    }

    public void Set(bool runOnStartup)
    {
        string filePath = process.MainModule.FileName;
        var registryKey = filePath.StartsWith(Environment.GetEnvironmentVariable("USERPROFILE"))
            ? Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true)
            : Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

        if (runOnStartup)
        {
            registryKey.SetValue(process.ProcessName, $"\"{filePath}\"");
        }
        else
        {
            registryKey.DeleteValue(process.ProcessName);
        }
    }
}
