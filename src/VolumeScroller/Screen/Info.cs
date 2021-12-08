using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VolumeScroller.Screen;

public class Info
{
    private readonly ImmutableList<Screen> screens;

    public Info()
        => screens = GetScreens();

    public bool OnTaskbar(Point point)
        => screens.Any(s => s.OnTaskbar(point));

    private static ImmutableList<Screen> GetScreens()
        => System.Windows.Forms.Screen.AllScreens.Cast<System.Windows.Forms.Screen>()
                                                 .Select(s => new Screen(new Rect(s.Bounds.X, s.Bounds.Y, s.Bounds.Width, s.Bounds.Height),
                                                                         new Rect(s.WorkingArea.X, s.WorkingArea.Y, s.WorkingArea.Width, s.WorkingArea.Height)))
                                                 .ToImmutableList();
}