using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeScroller;

public static class CursorInfo2
{
    public static bool IsOnTaskbar()
    {
        var cursorPosition = GetCursorPosition();

        return GetTaskbars().Select(i => GetRectangle(i))
                            .Where(r => r != null)
                            .Cast<Rectangle>()
                            .Any(i => i.Contains(cursorPosition));
    }

    static List<IntPtr> GetTaskbars()
    {
        Dictionary<IntPtr, string> windows = new();

        EnumWindows(delegate (IntPtr hWnd, int lParam)
        {
            string className = GetClassName(hWnd);

            if (!className.StartsWith("Shell_") || !className.EndsWith("TrayWnd"))
            {
                return true;
            }

            windows[hWnd] = className;
            return true;

        }, 0);

        return windows.Select(i => i.Key).ToList();
    }

    static Rectangle? GetRectangle(IntPtr hWnd) 
        => GetWindowRect(hWnd, out RECT lpRect) 
            ? new Rectangle(lpRect.Left, lpRect.Top, lpRect.Right - lpRect.Left, lpRect.Bottom - lpRect.Top) 
            : null;

    static string GetClassName(IntPtr hWnd)
    {
        StringBuilder ClassName = new(256);
        int nRet = GetClassName(hWnd, ClassName, ClassName.Capacity);

        return nRet == 0 ? string.Empty : ClassName.ToString();
    }

    static Point GetCursorPosition()
    {
        POINT point;
        GetCursorPos(out point);

        return new Point(point.X, point.Y);
    }

    delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

    [DllImport("USER32.DLL")]
    static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

    [DllImport("USER32.DLL")]
    static extern IntPtr GetShellWindow();

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
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
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner
    }
}
