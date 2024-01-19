using Android.App;
using Android.Runtime;
using PixSmith.Greenies.Mobile;

namespace PixSmith.Greenies.Mobile.Platforms.Android;

[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(nint handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
