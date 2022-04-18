using System.Windows;

namespace VolumeScroller;

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

    protected override void OnClosing(CancelEventArgs e)
    {
        e.Cancel = true;
        HideWindow();
    }

    void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        => Application.Current.Shutdown();

    void CloseButton_Click(object sender, RoutedEventArgs e)
        => HideWindow();

    void TaskbarIcon_TrayLeftMouseUp(object sender, RoutedEventArgs e)
        => ToggleVisibility();

    void SettingsMenuItem_Click(object sender, RoutedEventArgs e)
        => ShowWindow();

    void HideWindow()
    {
        Visibility = Visibility.Hidden;
        ShowInTaskbar = false;
        isHidden = true;
    }

    void ShowWindow()
    {
        Visibility = Visibility.Visible;
        ShowInTaskbar = true;
        Show();
        Activate();
        isHidden = false;
    }

    void ToggleVisibility()
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