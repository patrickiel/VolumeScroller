﻿namespace VolumeScroller;

public class MainViewModel : IDisposable
{
    readonly MainModel mainModel;

    public MainViewModel(MainModel mainModel)
    {
        this.mainModel = mainModel;
        SystemEvents.UserPreferenceChanged += UserPreferenceChanged;
    }

    public string TaskBarIconPath => MainModel.TaskBarIconPath;

    public bool RunOnStartup
    {
        get => mainModel.RunOnStartup;
        set => mainModel.RunOnStartup = value;
    }

    public bool TaskbarMustBeVisible
    {
        get => mainModel.TaskbarMustBeVisible;
        set => mainModel.TaskbarMustBeVisible = value;
    }

    public static int Increment
    {
        get => MainModel.Increment * 2;
        set => MainModel.Increment = value / 2;
    }

    public void Dispose()
        => SystemEvents.UserPreferenceChanged -= UserPreferenceChanged;

    private void UserPreferenceChanged(object sender, EventArgs e)
        => OnPropertyChanged(nameof(TaskBarIconPath));


    public event PropertyChangedEventHandler PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

}
