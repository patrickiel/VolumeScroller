namespace VolumeScroller;

public class AudioController : IDisposable
{
    private readonly MouseHook mouseHook;

    public AudioController(MainViewModel viewModel)
    {
        mouseHook = new();
        mouseHook.MouseWheelUp += _ => ChangeVolume(() => AudioControllerNative.VolumeUp());
        mouseHook.MouseWheelDown += _ => ChangeVolume(() => AudioControllerNative.VolumeDown());
        mouseHook.Initialize(Applies);
    }

    private static void ChangeVolume(Action action)
    {
        bool applies = Applies();

        int increment = Properties.Settings.Default.Increment;

        if (applies)
        {
            for (int i = 1; i <= increment; i++)
            {
                action();
            }
        }
    }

    private static bool Applies() => Properties.Settings.Default.TriggerMode switch
    {
        0 => CursorInfo1.IsOnTaskbar(),
        1 => CursorInfo2.IsOnTaskbar(),
        2 => CursorInfoEdges.IsOnScreenEdges(Properties.Settings.Default.EnableBottomLeft,
                                             Properties.Settings.Default.EnableTopLeft,
                                             Properties.Settings.Default.EnableTopRight,
                                             Properties.Settings.Default.EnableBottomRight,
                                             Properties.Settings.Default.EdgeTolerance),
        _ => false,
    };

    public void Dispose()
    {
        mouseHook.Terminate();
    }
}
