namespace VolumeScroller;

public static class CursorInfo1
{
    private static readonly List<string> classNames = new() { "MSTaskListWClass", "Start", "InputIndicatorButton", "MSTaskSwWClass", "ToolbarWindow32", "TrayClockWClass", "TrayButton", "ClockButton" };

    public static bool IsOnTaskbar()
    {
        GetCursorPos(out Point point);
        var hWnd = WindowFromPoint(point);
        StringBuilder stringBuilder = new(256);

        string className = GetClassName(hWnd, stringBuilder, stringBuilder.Capacity) != 0
            ? stringBuilder.ToString()
            : string.Empty;

        Debug.Print(className);

        return classNames.Contains(className);
    }

    [DllImport("user32.dll")]
    static extern bool GetCursorPos(out Point lpPoint);

    [DllImport("user32.dll")]
    static extern IntPtr WindowFromPoint(Point point);

    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
}