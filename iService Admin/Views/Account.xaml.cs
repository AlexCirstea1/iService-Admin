using iService_Admin.Models;

namespace iService_Admin.Views;

public partial class Account : ContentPage
{
	public Account()
    {
        InitializeComponent();
        Load();
	}

    private async void Load()
    {
        usernameLabel.Text = await SecureStorage.Default.GetAsync("ServiceName");
        ServiceLogo.Source = ImageStorage.ServiceLogo;
    }

    private async void LogOutButton_OnClickedButton_Clicked(object sender, EventArgs e)
    {
        await SecureStorage.Default.SetAsync("isLogged", "false");
        SecureStorage.Default.Remove("ServiceName");
        SecureStorage.Default.Remove("Address");
        SecureStorage.Default.Remove("Email");
        SecureStorage.Default.Remove("PhoneNumber");
        SecureStorage.Default.Remove("ServiceId");
        var mainPage = new MainPage();
        if (Application.Current != null) Application.Current.MainPage = mainPage;
    }

    private void Switch1_OnToggled(object sender, ToggledEventArgs e)
    {
        
    }

    private void ResetPasswordBtn(object sender, EventArgs e)
    {
        
    }
}