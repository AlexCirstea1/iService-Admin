namespace iService_Admin.Views;

public partial class Account : ContentPage
{
	public Account()
    {
        //Load();
		InitializeComponent();
	}

    private async void Load()
    {
        usernameLabel.Text = await SecureStorage.Default.GetAsync("ServiceName");
    }

    private void LogOutButton_OnClickedButton_Clicked(object sender, EventArgs e)
    {
        
    }

    private void Switch1_OnToggled(object sender, ToggledEventArgs e)
    {
        
    }

    private void ResetPasswordBtn(object sender, EventArgs e)
    {
        
    }
}