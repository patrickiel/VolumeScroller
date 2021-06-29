using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseConductor
{
    public static class AudioControllerNative
    {
        private const byte MuteCode = 0xAD;
        private const byte VolumeDownCode = 0xAE;
        private const byte VolumeUpCode = 0xAF;
        private const uint ExtendedKey = 0x0001;
        private const uint KeyUp = 0x0002;

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern byte MapVirtualKey(uint uCode, uint uMapType);

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
            keybd_event(MuteCode, MapVirtualKey(MuteCode, 0), ExtendedKey, 0);
            keybd_event(MuteCode, MapVirtualKey(MuteCode, 0), ExtendedKey | KeyUp, 0);
        }
    }
}
