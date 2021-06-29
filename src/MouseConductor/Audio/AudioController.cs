using Ikst.MouseHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MouseConductor
{
    public class AudioController : IDisposable
    {
        private readonly MouseHook mouseHook;

        public AudioController(Screen.Info screenInfo)
        {
            mouseHook = new();

            mouseHook.MouseWheel += st =>
            {
                bool onTaskbar = screenInfo.OnTaskbar(new Point(st.pt.x, st.pt.y));
                int increment = Properties.Settings.Default.Increment;

                if (st.mouseData == 7864320 && onTaskbar)
                {
                    for (int i = 1; i <= increment; i++)
                    {
                        AudioControllerNative.VolumeUp();
                    }
                }
                else if (st.mouseData == 4287102976 && onTaskbar)
                {
                    for (int i = 1; i <= increment; i++)
                    {
                        AudioControllerNative.VolumeDown();
                    }
                }
            };

            mouseHook.Start();
        }

        public void Dispose() 
            => mouseHook.Stop();
    }
}
