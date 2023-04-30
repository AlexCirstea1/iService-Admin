using iService_Admin.Models;
using iService_Admin.Services;

namespace iService_Admin.Views;

public partial class Home : ContentPage
{
    private readonly ShopService _shopService;
	public Home()
    {
        _shopService = new();
		InitializeComponent();
        Load();

    }

    private async void Load()
    {
        ShopNameLabel.Text = await SecureStorage.Default.GetAsync("ServiceName");
        try
        {
            var serviceId = Int32.Parse(await SecureStorage.Default.GetAsync("ServiceId"));
            var appointments = await _shopService.GetAppointmentsByServiceId(serviceId);
            foreach (var item in appointments)
            {
                item.CarName = item.Car.Manufacturer;
                item.Client = item.User.Username;
            }
            AppointmentsListView.ItemsSource = appointments;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error loading appointments: {ex.Message}", "OK");
        }
    }

    private void AppointmentsListView_OnRefreshing(object sender, EventArgs e)
    {
        Load();
    }

    private async void AppointmentsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is Appointment appointment)
        {
            var appointmentPage = new AppointmentPage(appointment);
            await Navigation.PushModalAsync(appointmentPage);
        }
        ((ListView)sender).SelectedItem = null; // Set the selected item to null to remove the highlight
    }

}