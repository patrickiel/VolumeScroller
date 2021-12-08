﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VolumeScroller;

public class MainModel
{
    public MainModel()
    {
    }

    public bool DarkTaskbar
    {
        get => Properties.Settings.Default.DarkTaskbar;
        set
        {
            Properties.Settings.Default.DarkTaskbar = value;
            Properties.Settings.Default.Save();
        }
    }

    public int Increment
    {
        get => Properties.Settings.Default.Increment;
        set
        {
            Properties.Settings.Default.Increment = value;
            Properties.Settings.Default.Save();
        }
    }
    public bool RunOnStartup
    {
        get => Properties.Settings.Default.RunOnStartup;
        set
        {
            Properties.Settings.Default.DarkTaskbar = value;
            Properties.Settings.Default.Save();
            new StartupManager(Process.GetCurrentProcess()).Set(value);
        }
    }
}
