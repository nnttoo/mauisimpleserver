namespace MauiAndroidServer;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();



    }

#if WINDOWS
    protected override Window CreateWindow(IActivationState? activationState)
    {
        var w = base.CreateWindow(activationState);
        w.Height = 800;
        w.Width = w.Height * 0.56;
        return w;
    }
#endif
}
