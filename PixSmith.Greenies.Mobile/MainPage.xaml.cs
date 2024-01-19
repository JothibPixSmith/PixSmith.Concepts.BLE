using PixSmith.Greenies.Mobile.ViewModels;

namespace PixSmith.Geenies.Mobile;

public partial class MainPage : ContentPage
{
    public MainPage(MainPageViewModel mainPageViewModel)
    {
        BindingContext = mainPageViewModel;
        InitializeComponent();
    }
}

