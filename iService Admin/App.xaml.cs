using iService_Admin.Models;
using iService_Admin.Services;
using Newtonsoft.Json;

namespace iService_Admin;

public partial class App : Application
{
    private readonly ShopService _shopService = new();
	public App()
	{
		InitializeComponent();
        if (Preferences.ContainsKey("IsLoggedIn") && Preferences.Get("IsLoggedIn", false))
        {
            MainPage = new AppShell();
        }
        else
        {
            SecureStorage.Default.Remove("ServiceName");
            SecureStorage.Default.Remove("Address");
            SecureStorage.Default.Remove("Email");
            SecureStorage.Default.Remove("PhoneNumber");
            SecureStorage.Default.Remove("ServiceId");
            ImageStorage.RemoveServiceLogo();
            MainPage = new MainPage();
        }
    }
}
