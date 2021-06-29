using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MouseConductor
{
    public class MainModel
    {
        public MainModel()
        {
        }

        public static bool DarkModeActive
        {
            get => Properties.Settings.Default.DarkModeActive;
            set
            {
                Properties.Settings.Default.DarkModeActive = value;
                Properties.Settings.Default.Save();
            }
        }

        public static int Increment
        {
            get => Properties.Settings.Default.Increment;
            set
            {
                Properties.Settings.Default.Increment = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
