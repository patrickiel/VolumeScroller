namespace VolumeScroller;

using System.Windows.Forms;

public static class CursorInfoCorners
{
    public static bool IsOnScreenCorners(bool bottomLeft, bool topLeft, bool topRight, bool bottomRight, int tolerance)
    {
        Point cursorPos = GetCursorPosition();

        foreach (Screen screen in Screen.AllScreens)
        {
            Rectangle rect = screen.Bounds;

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
