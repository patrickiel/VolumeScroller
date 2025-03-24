using System;
using System.Runtime.InteropServices;

class AudioMuteChecker
{
    // Core Audio Interfaces
    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IMMDeviceEnumerator
    {
        // Skip methods we don't need
        [PreserveSig]
        int NotImpl1();

        [PreserveSig]
        int GetDefaultAudioEndpoint(EDataFlow dataFlow, ERole role, out IMMDevice ppEndpoint);
    }

    [Guid("D666063F-1587-4E43-81F1-B948E807363F"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IMMDevice
    {
        [PreserveSig]
        int Activate(ref Guid iid, uint dwClsCtx, IntPtr pActivationParams, out IntPtr ppInterface);
    }

    [Guid("657804FA-D6AD-4496-8A60-352752AF4F89"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IAudioEndpointVolumeCallback
    {
        void OnNotify(IntPtr pNotifyData);
    }

    [Guid("5CDF2C82-841E-4546-9722-0CF74078229A"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    private interface IAudioEndpointVolume
    {
        [PreserveSig]
        int RegisterControlChangeNotify(IAudioEndpointVolumeCallback pNotify);

        [PreserveSig]
        int UnregisterControlChangeNotify(IAudioEndpointVolumeCallback pNotify);

        [PreserveSig]
        int GetChannelCount(out int pnChannelCount);

        [PreserveSig]
        int SetMasterVolumeLevel(float fLevelDB, Guid pguidEventContext);

        [PreserveSig]
        int SetMasterVolumeLevelScalar(float fLevel, Guid pguidEventContext);

        [PreserveSig]
        int GetMasterVolumeLevel(out float pfLevelDB);

        [PreserveSig]
        int GetMasterVolumeLevelScalar(out float pfLevel);

        [PreserveSig]
        int SetChannelVolumeLevel(uint nChannel, float fLevelDB, Guid pguidEventContext);

        [PreserveSig]
        int SetChannelVolumeLevelScalar(uint nChannel, float fLevel, Guid pguidEventContext);

        [PreserveSig]
        int GetChannelVolumeLevel(uint nChannel, out float pfLevelDB);

        [PreserveSig]
        int GetChannelVolumeLevelScalar(uint nChannel, out float pfLevel);

        [PreserveSig]
        int SetMute([MarshalAs(UnmanagedType.Bool)] bool bMute, Guid pguidEventContext);

        [PreserveSig]
        int GetMute(out bool pbMute);
    }

    [ComImport, Guid("BCDE0395-E52F-467C-8E3D-C4579291692E")]
    private class MMDeviceEnumerator { }

    private enum EDataFlow
    {
        eRender,
        eCapture,
        eAll,
        EDataFlow_enum_count
    }

    private enum ERole
    {
        eConsole,
        eMultimedia,
        eCommunications,
        ERole_enum_count
    }

    // Use a more direct approach with fewer interfaces
    public static bool IsSystemMuted()
    {
        try
        {
            // Create device enumerator
            IMMDeviceEnumerator deviceEnumerator = (IMMDeviceEnumerator)new MMDeviceEnumerator();

            // Get default audio endpoint
            int hr = deviceEnumerator.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eMultimedia, out IMMDevice device);
            if (hr != 0)
            {
                Debug.Print($"Failed to get default audio endpoint. HRESULT: 0x{hr:X}");
                return false;
            }

            // Activate IAudioEndpointVolume interface
            Guid iid = typeof(IAudioEndpointVolume).GUID;
            hr = device.Activate(ref iid, 1, IntPtr.Zero, out nint volumeControlPtr);
            if (hr != 0)
            {
                Debug.Print($"Failed to activate endpoint volume. HRESULT: 0x{hr:X}");
                Marshal.ReleaseComObject(device);
                return false;
            }

            // Get IAudioEndpointVolume from pointer
            IAudioEndpointVolume volumeControl = (IAudioEndpointVolume)Marshal.GetObjectForIUnknown(volumeControlPtr);
            Marshal.Release(volumeControlPtr);

            // Get mute state
            hr = volumeControl.GetMute(out bool isMuted);
            if (hr != 0)
            {
                Debug.Print($"Failed to get mute state. HRESULT: 0x{hr:X}");
                Marshal.ReleaseComObject(volumeControl);
                Marshal.ReleaseComObject(device);
                return false;
            }

            // Release COM objects properly
            Marshal.ReleaseComObject(volumeControl);
            Marshal.ReleaseComObject(device);
            Marshal.ReleaseComObject(deviceEnumerator);

            // Add extra debugging information
            Debug.Print($"DEBUG - Mute state detected: {isMuted}");

            return isMuted;
        }
        catch (Exception ex)
        {
            Debug.Print($"Exception checking mute state: {ex.Message}");
            Debug.Print($"Stack trace: {ex.StackTrace}");
            return false;
        }
    }
}