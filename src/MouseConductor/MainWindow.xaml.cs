using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MouseConductor
{
    /// <summary>
    /// Interaktionslogik für Settings.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isHidden;

        public MainWindow(MainViewModel mainViewModel)
        {
            InitializeComponent();
            DataContext = mainViewModel;
            HideWindow();
        }

        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
            => Application.Current.Shutdown();

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            HideWindow();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
            => HideWindow();

        private void TaskbarIcon_TrayLeftMouseUp(object sender, RoutedEventArgs e) 
            => ToggleVisibility();

        private void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
            => ShowWindow();

        private void HideWindow()
        {
            Visibility = Visibility.Hidden;
            ShowInTaskbar = false;
            isHidden = true;
        }

        private void ShowWindow()
        {
            Visibility = Visibility.Visible;
            ShowInTaskbar = true;
            Show();
            Activate();
            isHidden = false;
        }

        private void ToggleVisibility()
        {
            if (isHidden)
            {
                ShowWindow();
            }
            else
            {
                HideWindow();
            }
        }
    }
}