using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VolumeScroller
{
    public class MainViewModel : ViewModelBase
    {
        private readonly MainModel mainModel;
        private bool darkTaskbar;
        private int increment;

        public MainViewModel(MainModel mainModel)
        {
            this.mainModel = mainModel;
        }

        public string TaskBarIconPath
            => DarkTaskbar
                ? "/Resources/VolumeScroller_dark.ico"
                : "/Resources/VolumeScroller_light.ico";

        public bool DarkTaskbar
        {
            get => MainModel.DarkTaskbar;
            set
            {
                SetProperty(ref darkTaskbar, value);
                MainModel.DarkTaskbar = value;
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
