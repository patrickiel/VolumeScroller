namespace VolumeScroller;

public class MouseHook
{
    const int WH_MOUSE_LL = 14;

    const int WM_MOUSEWHEEL = 0x020A;
    const int WM_MBUTTONDOWN = 0x0207;
    const int WM_MBUTTONUP = 0x0208;

    MouseHookHandler hookHandler;
    IntPtr hookID = IntPtr.Zero;
    Func<bool> isRelevant;

    bool initialized;

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

    public event MouseHookCallback MouseWheelDown;
    public event MouseHookCallback MouseWheelUp;
    public event MouseHookCallback MiddleButtonDown;
    public event MouseHookCallback MiddleButtonUp;

    public void Initialize(Func<bool> isRelevant)
    {
        if (!initialized)
        {
            hookHandler = HookFunc;
            hookID = SetHook(hookHandler);
            this.isRelevant = isRelevant;

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
        if (nCode >= 0 && isRelevant())
        {
            switch ((int)wParam)
            {
                case WM_MOUSEWHEEL:
                    var data = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));

                    if (data.mouseData == 7864320)
                    {
                        MouseWheelUp?.Invoke(data);
                        return new IntPtr(1); // suppresses native behaviour
                    }
                    else if (data.mouseData == 4287102976)
                    {
                        MouseWheelDown?.Invoke(data);
                        return new IntPtr(1); // suppresses native behaviour
                    }
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