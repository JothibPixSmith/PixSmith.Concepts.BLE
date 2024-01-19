using Foundation;
using PixSmith.Greenies.Mobile;

namespace PixSmith.Greenies.Mobile.Platforms.iOS;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
