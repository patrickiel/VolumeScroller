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
            darkTaskbar = mainModel.DarkTaskbar;
            increment = mainModel.Increment * 2;
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
}
