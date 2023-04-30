using iService_Admin.Services;
using iService_Admin.Views;

namespace iService_Admin;

public partial class MainPage : ContentPage
{
    private readonly ShopService _shopService = new ShopService();

    public MainPage()
	{
		InitializeComponent();
	}


    private async void LoginButton_OnClicked(object sender, EventArgs e)
    {
        string email = UsernameEntry.Text;

        var serviceData = await _shopService.Login(email, "admin");
        if (serviceData != null)
        {
            await SecureStorage.Default.SetAsync("ServiceName", serviceData.ServiceName);
            await SecureStorage.Default.SetAsync("Address", serviceData.Address);
            await SecureStorage.Default.SetAsync("Email", serviceData.Email);
            await SecureStorage.Default.SetAsync("PhoneNumber", serviceData.PhoneNumber);
            await SecureStorage.Default.SetAsync("ServiceId", serviceData.ServiceId.ToString());

            var homePage = new AppShell();
            Application.Current.MainPage = homePage;
        }
        else
        {
            DisplayAlert("Failed", "Login has failed", "ok");
        }
    }

    private void Button_OnClicked(object sender, EventArgs e)
    {
        
    }
}

