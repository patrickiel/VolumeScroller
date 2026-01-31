namespace VolumeScroller;

using System.Windows.Forms;

public static class CursorInfoEdges
{
    public static bool IsOnScreenEdges(bool top, bool right, bool bottom, bool left, int tolerance)
    {
        Point cursorPos = GetCursorPosition();

        foreach (Screen screen in Screen.AllScreens)
        {
            Rectangle rect = screen.Bounds;

            if (IsNearSelectedEdges(cursorPos, rect, top, right, bottom, left, tolerance))
                return true;
        }

        return false;
    }

    private static bool IsNearSelectedEdges(Point cursorPos, Rectangle rect, bool top, bool right, bool bottom, bool left, int tolerance)
    {
        if (top)
        {
            bool nearTop = cursorPos.Y >= rect.Top &&
                           cursorPos.Y <= rect.Top + tolerance &&
                           cursorPos.X >= rect.Left &&
                           cursorPos.X <= rect.Right;
            if (nearTop) return true;
        }

        if (right)
        {
            bool nearRight = cursorPos.X >= rect.Right - tolerance &&
                             cursorPos.X <= rect.Right &&
                             cursorPos.Y >= rect.Top &&
                             cursorPos.Y <= rect.Bottom;
            if (nearRight) return true;
        }

        if (bottom)
        {
            bool nearBottom = cursorPos.Y >= rect.Bottom - tolerance &&
                              cursorPos.Y <= rect.Bottom &&
                              cursorPos.X >= rect.Left &&
                              cursorPos.X <= rect.Right;
            if (nearBottom) return true;
        }

        if (left)
        {
            bool nearLeft = cursorPos.X >= rect.Left &&
                            cursorPos.X <= rect.Left + tolerance &&
                            cursorPos.Y >= rect.Top &&
                            cursorPos.Y <= rect.Bottom;
            if (nearLeft) return true;
        }

        return false;
    }

    private static Point GetCursorPosition()
    {
        GetCursorPos(out POINT point);
        return new Point(point.X, point.Y);
    }

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetCursorPos(out POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int X;
        public int Y;
    }
}
