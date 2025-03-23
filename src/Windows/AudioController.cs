namespace VolumeScroller;

using System;

public class AudioController : IDisposable
{
    private readonly MouseHook mouseHook;
    private readonly MainViewModel viewModel;
    private DateTime lastMuteAction = DateTime.MinValue;
    private static readonly TimeSpan MuteDebounceInterval = TimeSpan.FromMilliseconds(300);

    public AudioController(MainViewModel viewModel)
    {
        this.viewModel = viewModel;
        mouseHook = new();
        mouseHook.MouseWheelUp += _ => ChangeVolume(() => AudioControllerNative.VolumeUp());
        mouseHook.MouseWheelDown += _ => ChangeVolume(() => AudioControllerNative.VolumeDown());
        
        // Add handlers for Ctrl+Scroll events - scroll down to mute, scroll up to unmute
        mouseHook.CtrlMouseWheelUp += _ => HandleMuteControl(false); // Unmute on scroll up
        mouseHook.CtrlMouseWheelDown += _ => HandleMuteControl(true); // Mute on scroll down
        
        mouseHook.Initialize(Applies);
    }

    private void HandleMuteControl(bool shouldMute)
    {
        // Only handle mute control if the feature is enabled
        if (!Properties.Settings.Default.EnableCtrlMute) return;
        
        bool applies = Applies();
        
        if (applies)
        {
            // Implement debounce to prevent rapid toggling when scrolling fast
            DateTime now = DateTime.Now;
            if (now - lastMuteAction < MuteDebounceInterval)
            {
                return;
            }
            
            lastMuteAction = now;
            
            if (shouldMute)
            {
                AudioControllerNative.Mute();
            }
            else
            {
                AudioControllerNative.Unmute();
            }
        }
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
        0 => CursorInfoTaskbarAlways.IsOnTaskbar(),
        1 => CursorInfoTaskbarVisible.IsOnTaskbar(),
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
