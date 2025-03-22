using Microsoft.Win32;

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace VolumeScroller;
public class MainViewModel : INotifyPropertyChanged, IDisposable
{
    readonly MainModel mainModel;

    public MainViewModel(MainModel mainModel)
    {
        this.mainModel = mainModel;
        SystemEvents.UserPreferenceChanged += UserPreferenceChanged;
    }

    public string TaskBarIconPath => MainModel.TaskBarIconPath;

    public bool RunOnStartup
    {
        get => mainModel.RunOnStartup;
        set => mainModel.RunOnStartup = value;
    }

    public int Increment
    {
        get => MainModel.Increment * 2;
        set => MainModel.Increment = value / 2;
    }

    public TriggerMode Mode
    {
        get => mainModel.Mode;
        set
        {
            if (mainModel.Mode != value)
            {
                mainModel.Mode = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsEdgesModeSelected));
            }
        }
    }

    public bool EnableTopLeft
    {
        get => mainModel.EnableTopLeft;
        set
        {
            if (mainModel.EnableTopLeft != value)
            {
                mainModel.EnableTopLeft = value;
                OnPropertyChanged();
            }
        }
    }

    public bool IsEdgesModeSelected => Mode == TriggerMode.ScreenEdges;

    public bool EnableTopRight
    {
        get => mainModel.EnableTopRight;
        set
        {
            if (mainModel.EnableTopRight != value)
            {
                mainModel.EnableTopRight = value;
                OnPropertyChanged();
            }
        }
    }

    public bool EnableBottomLeft
    {
        get => mainModel.EnableBottomLeft;
        set
        {
            if (mainModel.EnableBottomLeft != value)
            {
                mainModel.EnableBottomLeft = value;
                OnPropertyChanged();
            }
        }
    }

    public bool EnableBottomRight
    {
        get => mainModel.EnableBottomRight;
        set
        {
            if (mainModel.EnableBottomRight != value)
            {
                mainModel.EnableBottomRight = value;
                OnPropertyChanged();
            }
        }
    }

    public int EdgeTolerance
    {
        get => mainModel.EdgeTolerance;
        set
        {
            if (mainModel.EdgeTolerance != value)
            {
                mainModel.EdgeTolerance = value;
                OnPropertyChanged();
            }
        }
    }

    public bool EnableCtrlMute
    {
        get => mainModel.EnableCtrlMute;
        set
        {
            if (mainModel.EnableCtrlMute != value)
            {
                mainModel.EnableCtrlMute = value;
                OnPropertyChanged();
            }
        }
    }

    public void Dispose()
    {
        SystemEvents.UserPreferenceChanged -= UserPreferenceChanged;
    }

    private void UserPreferenceChanged(object sender, EventArgs e)
        => OnPropertyChanged(nameof(TaskBarIconPath));

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}