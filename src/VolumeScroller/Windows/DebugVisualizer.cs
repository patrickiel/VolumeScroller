using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Forms = System.Windows.Forms;

namespace VolumeScroller;

public class DebugVisualizer: IDisposable
{
    private readonly Window debugWindow;
    private readonly Canvas canvas;

    public DebugVisualizer()
    {
        debugWindow = new Window
        {
            WindowStyle = WindowStyle.None,
            AllowsTransparency = true,
            Background = System.Windows.Media.Brushes.Transparent,
            Topmost = true,
            ShowInTaskbar = false,
            ResizeMode = ResizeMode.NoResize
        };

        canvas = new Canvas();
        debugWindow.Content = canvas;
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }
} 