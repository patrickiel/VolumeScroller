using Ikst.MouseHook;

namespace VolumeScroller;

public class AudioController : IDisposable
{
    private readonly MouseHook mouseHook;

    public AudioController()
    {
        mouseHook = new();

        mouseHook.MouseWheel += st =>
        {
            bool onTaskbar = CursorInfo1.IsOnTaskbar();
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
