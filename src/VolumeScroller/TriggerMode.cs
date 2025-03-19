namespace VolumeScroller
{
    /// <summary>
    /// Defines the trigger modes for volume control
    /// </summary>
    public enum TriggerMode
    {
        /// <summary>
        /// Requires visible taskbar to function
        /// </summary>
        TaskbarOnly,

        /// <summary>
        /// Works with both visible and hidden taskbar
        /// </summary>
        TaskbarAlways,

        /// <summary>
        /// Uses screen edges for triggering volume control
        /// </summary>
        ScreenEdges
    }
}