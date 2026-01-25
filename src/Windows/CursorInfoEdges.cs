namespace VolumeScroller;

using System.Windows.Forms;

public static class CursorInfoEdges
{
    public static bool IsOnScreenEdges(bool bottomLeft, bool topLeft, bool topRight, bool bottomRight, int tolerance)
    {
        // Get the current cursor position
        Point cursorPos = GetCursorPosition();

        // Check all screens
        foreach (Screen screen in Screen.AllScreens)
        {
            Rectangle rect = screen.Bounds;

            // Check if the cursor is near the enabled edges for this screen
            if (bottomLeft && IsNearBottomLeft(cursorPos, rect, tolerance))
                return true;

            if (topLeft && IsNearTopLeft(cursorPos, rect, tolerance))
                return true;

            if (topRight && IsNearTopRight(cursorPos, rect, tolerance))
                return true;

            if (bottomRight && IsNearBottomRight(cursorPos, rect, tolerance))
                return true;
        }

        return false;
    }

    /// <summary>
    /// Returns true if the cursor is near any screen edge (full sides, not just corners)
    /// </summary>
    public static bool IsOnAnyScreenEdge(int tolerance)
    {
        // Get the current cursor position
        Point cursorPos = GetCursorPosition();
        
        // Check all screens
        foreach (Screen screen in Screen.AllScreens)
        {
            Rectangle rect = screen.Bounds;

            if (IsNearAnyEdge(cursorPos, rect, tolerance))
                return true;
        }

        return false;
    }

    private static bool IsNearAnyEdge(Point cursorPos, Rectangle rect, int tolerance)
    {
        bool nearTop = cursorPos.Y >= rect.Top &&
                       cursorPos.Y <= rect.Top + tolerance &&
                       cursorPos.X >= rect.Left &&
                       cursorPos.X <= rect.Right;

        bool nearRight = cursorPos.X >= rect.Right - tolerance &&
                         cursorPos.X <= rect.Right &&
                         cursorPos.Y >= rect.Top &&
                         cursorPos.Y <= rect.Bottom;

        bool nearBottom = cursorPos.Y >= rect.Bottom - tolerance &&
                          cursorPos.Y <= rect.Bottom &&
                          cursorPos.X >= rect.Left &&
                          cursorPos.X <= rect.Right;

        bool nearLeft = cursorPos.X >= rect.Left &&
                        cursorPos.X <= rect.Left + tolerance &&
                        cursorPos.Y >= rect.Top &&
                        cursorPos.Y <= rect.Bottom;

        return nearTop || nearRight || nearBottom || nearLeft;
    }

    private static bool IsNearBottomLeft(Point cursorPos, Rectangle rect, int tolerance)
    {
        return cursorPos.X >= rect.Left &&
               cursorPos.X <= rect.Left + tolerance &&
               cursorPos.Y >= rect.Bottom - tolerance &&
               cursorPos.Y <= rect.Bottom;
    }

    private static bool IsNearTopLeft(Point cursorPos, Rectangle rect, int tolerance)
    {
        return cursorPos.X >= rect.Left &&
               cursorPos.X <= rect.Left + tolerance &&
               cursorPos.Y >= rect.Top &&
               cursorPos.Y <= rect.Top + tolerance;
    }

    private static bool IsNearTopRight(Point cursorPos, Rectangle rect, int tolerance)
    {
        return cursorPos.X >= rect.Right - tolerance &&
               cursorPos.X <= rect.Right &&
               cursorPos.Y >= rect.Top &&
               cursorPos.Y <= rect.Top + tolerance;
    }

    private static bool IsNearBottomRight(Point cursorPos, Rectangle rect, int tolerance)
    {
        return cursorPos.X >= rect.Right - tolerance &&
               cursorPos.X <= rect.Right &&
               cursorPos.Y >= rect.Bottom - tolerance &&
               cursorPos.Y <= rect.Bottom;
    }

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