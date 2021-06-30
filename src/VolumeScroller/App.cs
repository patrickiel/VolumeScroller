using Ikst.MouseHook;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace VolumeScroller
{
    public partial class App : Application
    {
        private AudioController audioController;

        public void App_Startup(object sender, StartupEventArgs e)
        {
            InitializeTrayIcon();
            Screen.Info screenInfo = new();
            audioController = new AudioController(screenInfo);
        }

        private static void InitializeTrayIcon()
        {
            MainModel main = new();
            MainViewModel mainViewModel = new(main);
            MainWindow mainWindow = new(mainViewModel);
            mainWindow.Show();
        }
    }
}
