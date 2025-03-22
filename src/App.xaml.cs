using System.Data;
using System.Windows;
using System.Diagnostics;
using System.Linq;

namespace VolumeScroller;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private AudioController audioController;
    private MainViewModel mainViewModel;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        ShutdownIfAlreadyRunning();
        
        var mainModel = new MainModel();
        mainViewModel = new MainViewModel(mainModel);
        var mainWindow = new MainWindow(mainViewModel);
        audioController = new AudioController(mainViewModel);
        
        mainWindow.Show();
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

    private void Application_Exit(object sender, ExitEventArgs e)
    {
        mainViewModel?.Dispose();
        audioController?.Dispose();
    }
}