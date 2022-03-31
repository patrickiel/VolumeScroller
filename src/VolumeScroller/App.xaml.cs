using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace VolumeScroller;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private AudioController audioController;

    public void App_Startup(object sender, StartupEventArgs e)
    {
        ShutdownIfAlreadyRunning();
        InitializeTrayIcon();
        audioController = new();

        SystemEvents.DisplaySettingsChanged += DisplaySettingsChanged;
    }

    private void DisplaySettingsChanged(object sender, EventArgs e)
    {
        audioController?.Dispose();
        audioController = new();
    }

    private static void ShutdownIfAlreadyRunning()
    {
        var currentSessionID = Process.GetCurrentProcess().SessionId;
        Process currentProcess = Process.GetCurrentProcess();
        int count = Process.GetProcesses()
                           .Where(p => p.SessionId == currentSessionID)
                           .Count(p => p.ProcessName.Equals(currentProcess.ProcessName));

        if (count > 1)
        {
            Current.Shutdown();
        }
    }

    private static void InitializeTrayIcon()
    {
        MainModel main = new();
        MainViewModel mainViewModel = new(main);
        MainWindow mainWindow = new(mainViewModel);
        mainWindow.Show();
    }

    private void Application_Exit(object sender, ExitEventArgs e)
    {
        audioController.Dispose();
        SystemEvents.DisplaySettingsChanged -= DisplaySettingsChanged;
    }
}