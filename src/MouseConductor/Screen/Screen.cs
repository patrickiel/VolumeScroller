using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MouseConductor.Screen
{
    public record Screen
    {
        public Screen(Rect screenArea, Rect workingArea)
        {
            ScreenArea = screenArea;
            WorkingArea = workingArea;
        }

        public Rect ScreenArea { get; }

        public Rect WorkingArea { get; }

        public Rect TaskbarArea { get; }

        public bool OnTaskbar(Point point) 
            => ScreenArea.Contains(point) && 
               !WorkingArea.Contains(point);
    }
}
