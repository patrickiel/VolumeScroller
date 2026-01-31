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
        /// Uses screen corners for triggering volume control
        /// </summary>
        ScreenCorners,

        /// <summary>
        /// Uses screen edges for triggering volume control
        /// </summary>
        ScreenEdges
    }
}