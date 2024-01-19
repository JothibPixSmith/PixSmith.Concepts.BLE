using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PixSmith.Geenies.Mobile;
using PixSmith.Greenies.Mobile.Repositories;
using PixSmith.Greenies.Mobile.Repositories.Interfaces;
using PixSmith.Greenies.Mobile.Services;
using PixSmith.Greenies.Mobile.Services.Interfaces;
using Plugin.BLE;

namespace PixSmith.Greenies.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .RegisterViews()
            .RegisterAppServices()
            .RegisterAppRepositories()
            .RegisterThirdPartyServices()
            .RegisterViewModels()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<MainPage>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<ViewModels.MainPageViewModel>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IBleService, BleService>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterAppRepositories(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IBleRepository, BleRepository>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterThirdPartyServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton(CrossBluetoothLE.Current);

        return mauiAppBuilder;
    }
}
