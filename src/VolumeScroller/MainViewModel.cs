using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VolumeScroller;

public class MainViewModel : ViewModelBase
{
    private readonly MainModel mainModel;
    private bool darkTaskbar;
    private int increment;
    private bool runOnStartup;

    public MainViewModel(MainModel mainModel)
    {
        this.mainModel = mainModel;
        darkTaskbar = mainModel.DarkTaskbar;
        increment = mainModel.Increment * 2;
        runOnStartup = mainModel.RunOnStartup;
    }

    public string TaskBarIconPath
        => DarkTaskbar
            ? "/Resources/VolumeScroller_dark.ico"
            : "/Resources/VolumeScroller_light.ico";

    public bool DarkTaskbar
    {
        get => darkTaskbar;
        set
        {
            SetProperty(ref darkTaskbar, value);
            mainModel.DarkTaskbar = value;
            OnPropertyChanged(nameof(TaskBarIconPath));
        }
    }

    public bool RunOnStartup
    {
        get => runOnStartup;
        set
        {
            SetProperty(ref runOnStartup, value);
            mainModel.RunOnStartup = value;
        }
    }

    public int Increment
    {
        get => increment;
        set
        {
            SetProperty(ref increment, value);
            mainModel.Increment = value / 2;
        }
    }
}
