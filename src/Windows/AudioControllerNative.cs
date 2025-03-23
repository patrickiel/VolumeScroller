namespace VolumeScroller;

public static class AudioControllerNative
{
    const byte MuteCode = 0xAD;
    const byte VolumeDownCode = 0xAE;
    const byte VolumeUpCode = 0xAF;
    const uint ExtendedKey = 0x0001;
    const uint KeyUp = 0x0002;
    
    // Track the current mute state internally
    private static bool isMuted = false;
    // Lock object to prevent concurrent state modifications
    private static readonly object muteLock = new();

    [DllImport("user32.dll")]
    static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

    [DllImport("user32.dll")]
    static extern byte MapVirtualKey(uint uCode, uint uMapType);

    public static void VolumeUp()
    {
        keybd_event(VolumeUpCode, MapVirtualKey(VolumeUpCode, 0), ExtendedKey, 0);
        keybd_event(VolumeUpCode, MapVirtualKey(VolumeUpCode, 0), ExtendedKey | KeyUp, 0);
    }

    public static void VolumeDown()
    {
        keybd_event(VolumeDownCode, MapVirtualKey(VolumeDownCode, 0), ExtendedKey, 0);
        keybd_event(VolumeDownCode, MapVirtualKey(VolumeDownCode, 0), ExtendedKey | KeyUp, 0);
    }

    public static void ToggleMute()
    {
        lock (muteLock)
        {
            keybd_event(MuteCode, MapVirtualKey(MuteCode, 0), ExtendedKey, 0);
            keybd_event(MuteCode, MapVirtualKey(MuteCode, 0), ExtendedKey | KeyUp, 0);
            isMuted = !isMuted; // Toggle the tracked state
        }
    }
    
    public static void Mute()
    {
        lock (muteLock)
        {
            if (!isMuted)
            {
                keybd_event(MuteCode, MapVirtualKey(MuteCode, 0), ExtendedKey, 0);
                keybd_event(MuteCode, MapVirtualKey(MuteCode, 0), ExtendedKey | KeyUp, 0);
                isMuted = true;
            }
        }
    }
    
    public static void Unmute()
    {
        lock (muteLock)
        {
            if (isMuted)
            {
                keybd_event(MuteCode, MapVirtualKey(MuteCode, 0), ExtendedKey, 0);
                keybd_event(MuteCode, MapVirtualKey(MuteCode, 0), ExtendedKey | KeyUp, 0);
                isMuted = false;
            }
        }
    }
    
    public static bool GetMuteState()
    {
        lock (muteLock)
        {
            return isMuted;
        }
    }
}
