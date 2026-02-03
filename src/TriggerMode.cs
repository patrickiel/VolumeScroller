namespace VolumeScroller
{
    /// <summary>
    /// Defines the trigger modes for volume control
    /// </summary>
    public enum TriggerMode
    {
        /// <summary>
        /// Works with both visible and hidden taskbar
        /// </summary>
        TaskbarAlways,

        /// <summary>
        /// Requires visible taskbar to function
        /// </summary>
        TaskbarVisibleOnly,

        /// <summary>
        /// Uses screen borders (edges and corners) for triggering volume control
        /// </summary>
        ScreenBorders
    }
}