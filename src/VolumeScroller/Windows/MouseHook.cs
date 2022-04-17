using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeScroller;

public class MouseHook
{
    const int WH_MOUSE_LL = 14;

    const int WM_MOUSEWHEEL = 0x020A;
    const int WM_MBUTTONDOWN = 0x0207;
    const int WM_MBUTTONUP = 0x0208;


    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MSLLHOOKSTRUCT
    {
        public POINT pt;
        public uint mouseData;
        public uint flags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    public delegate void MouseHookCallback(MSLLHOOKSTRUCT mouseStruct);
    public delegate IntPtr MouseHookHandler(int nCode, IntPtr wParam, IntPtr lParam);

    public event MouseHookCallback MouseWheel;
    public event MouseHookCallback MiddleButtonDown;
    public event MouseHookCallback MiddleButtonUp;

    MouseHookHandler hookHandler;
    IntPtr hookID = IntPtr.Zero;

    bool initialized;

    public void Initialize()
    {
        if (!initialized)
        {
            hookHandler = HookFunc;
            hookID = SetHook(hookHandler);

            initialized = true;
        }
    }

    public void Terminate()
    {
        if (initialized)
        {
            if (hookID == IntPtr.Zero) return;

            UnhookWindowsHookEx(hookID);
            hookID = IntPtr.Zero;

            initialized = false;
        }
    }

    ~MouseHook()
    {
        Terminate();
    }


    private static IntPtr SetHook(MouseHookHandler proc)
    {
        using ProcessModule module = Process.GetCurrentProcess().MainModule;
        return SetWindowsHookEx(WH_MOUSE_LL, proc, GetModuleHandle(module.ModuleName), 0);
    }


    private IntPtr HookFunc(int nCode, IntPtr wParam, IntPtr lParam)
    {

        if (nCode >= 0)
        {
            switch ((int)wParam)
            {

                case WM_MOUSEWHEEL:
                    MouseWheel?.Invoke((MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT)));
                    break;

                case WM_MBUTTONDOWN:
                    MiddleButtonDown?.Invoke((MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT)));
                    break;

                case WM_MBUTTONUP:
                    MiddleButtonUp?.Invoke((MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT)));
                    break;

                default:
                    break;
            }
        }

        return CallNextHookEx(hookID, nCode, wParam, lParam);
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static extern IntPtr SetWindowsHookEx(int idHook, MouseHookHandler lpfn, IntPtr hMod, uint dwThreadId);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static extern bool UnhookWindowsHookEx(IntPtr hhk);

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    internal static extern IntPtr GetModuleHandle(string lpModuleName);
}
