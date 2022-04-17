namespace VolumeScroller;

public class AudioController : IDisposable
{
    private readonly MouseHook mouseHook;

    public AudioController()
    {
        mouseHook = new();

        mouseHook.MouseWheel += st =>
        {
            bool onTaskbar = Properties.Settings.Default.TaskbarMustBeVisible
                ? CursorInfo1.IsOnTaskbar()
                : CursorInfo2.IsOnTaskbar();

            int increment = Properties.Settings.Default.Increment;

            if (onTaskbar)
            {
                if (st.mouseData == 7864320)
                {
                    ExecuteNumberOfTimes(increment, () => AudioControllerNative.VolumeUp());
                }
                else if (st.mouseData == 4287102976)
                {
                    ExecuteNumberOfTimes(increment, () => AudioControllerNative.VolumeDown());
                }
            }
        };

        mouseHook.Initialize();
    }

    private static void ExecuteNumberOfTimes(int numberOfTimes, Action action)
    {
        for (int i = 1; i <= numberOfTimes; i++)
        {
            action();
        }
    }

    public void Dispose()
        => mouseHook.Terminate();
}
