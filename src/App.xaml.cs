using System.Data;
using System.Windows;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace VolumeScroller;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private AudioController audioController;
    private MainViewModel mainViewModel;
    private Mutex appMutex;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        if (!EnsureSingleInstance())
        {
            // Another instance is already running, exit
            Current.Shutdown();
            return;
        }
        
        var mainModel = new MainModel();
        mainViewModel = new MainViewModel(mainModel);
        var mainWindow = new MainWindow(mainViewModel);
        audioController = new AudioController(mainViewModel);
        
        mainWindow.Show();
    }

    private bool EnsureSingleInstance()
    {
        // Use a global mutex name with a unique identifier for the application
        string mutexName = "VolumeScrollerSingleInstanceMutex";

        // Try to create or open the mutex
        appMutex = new Mutex(true, mutexName, out bool createdNew);

        // If the mutex was already created (createdNew is false), 
        // then another instance is running
        return createdNew;
    }

    private void Application_Exit(object sender, ExitEventArgs e)
    {
        mainViewModel?.Dispose();
        audioController?.Dispose();
        
        // Release the mutex when the application exits
        appMutex?.ReleaseMutex();
        appMutex?.Dispose();
    }
}