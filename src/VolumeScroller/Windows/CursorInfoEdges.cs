namespace VolumeScroller;
public static class CursorInfoEdges
{
    public static bool IsOnScreenEdges(bool bottomLeft, bool topLeft, bool topRight, bool bottomRight, int tollerance)
    {
        // Get the current cursor position
        Point cursorPos = GetCursorPosition();

        // Get the current active window rectangle
        Rectangle? activeWindowRect = GetActiveWindowRect();

        if (activeWindowRect == null)
            return false;

        Rectangle rect = activeWindowRect.Value;

        // Check if the cursor is near the enabled edges
        if (bottomLeft && IsNearBottomLeft(cursorPos, rect, tollerance))
            return true;

        if (topLeft && IsNearTopLeft(cursorPos, rect, tollerance))
            return true;

        if (topRight && IsNearTopRight(cursorPos, rect, tollerance))
            return true;

        if (bottomRight && IsNearBottomRight(cursorPos, rect, tollerance))
            return true;

        return false;
    }

    private static bool IsNearBottomLeft(Point cursorPos, Rectangle rect, int tollerance)
    {
        return cursorPos.X >= rect.Left - tollerance &&
               cursorPos.X <= rect.Left + tollerance &&
               cursorPos.Y >= rect.Bottom - tollerance &&
               cursorPos.Y <= rect.Bottom + tollerance;
    }

    private static bool IsNearTopLeft(Point cursorPos, Rectangle rect, int tollerance)
    {
        return cursorPos.X >= rect.Left - tollerance &&
               cursorPos.X <= rect.Left + tollerance &&
               cursorPos.Y >= rect.Top - tollerance &&
               cursorPos.Y <= rect.Top + tollerance;
    }

    private static bool IsNearTopRight(Point cursorPos, Rectangle rect, int tollerance)
    {
        return cursorPos.X >= rect.Right - tollerance &&
               cursorPos.X <= rect.Right + tollerance &&
               cursorPos.Y >= rect.Top - tollerance &&
               cursorPos.Y <= rect.Top + tollerance;
    }

    private static bool IsNearBottomRight(Point cursorPos, Rectangle rect, int tollerance)
    {
        return cursorPos.X >= rect.Right - tollerance &&
               cursorPos.X <= rect.Right + tollerance &&
               cursorPos.Y >= rect.Bottom - tollerance &&
               cursorPos.Y <= rect.Bottom + tollerance;
    }

    private static Rectangle? GetActiveWindowRect()
    {
        IntPtr activeWindow = GetForegroundWindow();
        return GetRectangle(activeWindow);
    }

    static Rectangle? GetRectangle(IntPtr hWnd)
        => GetWindowRect(hWnd, out RECT lpRect)
            ? new Rectangle(lpRect.Left, lpRect.Top, lpRect.Right - lpRect.Left, lpRect.Bottom - lpRect.Top)
            : null;

    static Point GetCursorPosition()
    {
        GetCursorPos(out POINT point);
        return new Point(point.X, point.Y);
    }

    delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

    [DllImport("USER32.DLL")]
    static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

    [DllImport("USER32.DLL")]
    static extern IntPtr GetShellWindow();

    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool GetCursorPos(out POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    struct POINT
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}