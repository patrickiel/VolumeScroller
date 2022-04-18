namespace VolumeScroller;

public class AudioController : IDisposable
{
    private readonly MouseHook mouseHook;

    public AudioController()
    {
        mouseHook = new();
        mouseHook.MouseWheelUp += _ => ChangeVolume(() => AudioControllerNative.VolumeUp());
        mouseHook.MouseWheelDown += _ => ChangeVolume(() => AudioControllerNative.VolumeDown());
        mouseHook.Initialize();
    }

    private static void ChangeVolume(Action action)
    {
        bool onTaskbar = Properties.Settings.Default.TaskbarMustBeVisible
                        ? CursorInfo1.IsOnTaskbar()
                        : CursorInfo2.IsOnTaskbar();

        int increment = Properties.Settings.Default.Increment;

        if (onTaskbar)
        {
            for (int i = 1; i <= increment; i++)
            {
                action();
            }
        }
    }

    public void Dispose()
        => mouseHook.Terminate();
}
