using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MouseConductor
{
    public class MainViewModel : ViewModelBase
    {
        private readonly MainModel mainModel;
        private bool darkModeActive;
        private int increment;

        public MainViewModel(MainModel mainModel)
        {
            this.mainModel = mainModel;
        }

        public string TaskBarIconPath
            => DarkModeActive
                ? "/Resources/VolumeScroller_light.ico"
                : "/Resources/VolumeScroller_dark.ico";

        public bool DarkModeActive
        {
            get => MainModel.DarkModeActive;
            set
            {
                SetProperty(ref darkModeActive, value);
                MainModel.DarkModeActive = value;
                OnPropertyChanged(nameof(TaskBarIconPath));
            }
        }

        public int Increment
        {
            get => MainModel.Increment * 2;
            set
            {
                SetProperty(ref increment, value);
                MainModel.Increment = value / 2;
            }
        }
    }
}
