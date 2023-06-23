using iService_Admin.Models;
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
        var email = UsernameEntry.Text;
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(email))
        {
            await DisplayAlert("Error", "Please enter a username", "OK");
            return;
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            await DisplayAlert("Error", "Please enter a password", "OK");
            return;
        }

        var serviceData = await _shopService.Login(email, password);
        if (serviceData != null)
        {
            Preferences.Set("IsLoggedIn", true);
            await SecureStorage.Default.SetAsync("ServiceName", serviceData.ServiceName);
            await SecureStorage.Default.SetAsync("Address", serviceData.Address);
            await SecureStorage.Default.SetAsync("Email", serviceData.Email);
            await SecureStorage.Default.SetAsync("PhoneNumber", serviceData.PhoneNumber);
            await SecureStorage.Default.SetAsync("ServiceId", serviceData.ServiceId.ToString());
            LoadImage(serviceData.ServiceId);
            var homePage = new AppShell();
            Application.Current.MainPage = homePage;
        }
        else
        {
            await DisplayAlert("Failed", "Login has failed", "ok");
        }
    }


    private async void LoadImage(int serviceId)
    {
        try
        {
            byte[] imageBytes = await _shopService.GetImage(serviceId);

            if (imageBytes != null)
            {
                var imageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                ImageStorage.ServiceLogo = imageSource;
            }
            else
            {
                Console.WriteLine("Image not found or error occurred.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading the image: {ex.Message}");
        }
    }


    private void Button_OnClicked(object sender, EventArgs e)
    {
        DisplayAlert("Register", "Please contact us at support@iservice.com for registering", "OK");
    }
}

