using iService_Admin.Models;
using iService_Admin.Services;

namespace iService_Admin.Views;

public partial class Home : ContentPage
{
    private readonly ShopService _shopService;
    public Home()
    {
        _shopService = new ShopService();
        InitializeComponent();
        Load();

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Load();
    }
    private async void Load()
    {
        try
        {
            var serviceId = int.Parse(await SecureStorage.Default.GetAsync("ServiceId"));
            var appointments = await _shopService.GetAppointmentsByServiceId(serviceId);
            appointments = appointments.Where(a => a.isConfirmed == false && a.AppointmentDate.Date > DateTime.Now.Date).OrderBy(a => a.AppointmentDate).ToList();
            foreach (var item in appointments)
            {
                item.CarName = item.Car.Manufacturer;
                item.Client = item.User.Username;
            }
            AppointmentsListView.ItemsSource = appointments;

            ServiceLogo.Source = ImageStorage.ServiceLogo;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error loading appointments: {ex.Message}", "OK");
        }
    }

    private async void AppointmentsListView_OnRefreshing(object sender, EventArgs e)
    {
        try
        {
            var serviceId = int.Parse(await SecureStorage.Default.GetAsync("ServiceId"));
            var appointments = await _shopService.GetAppointmentsByServiceId(serviceId);
            appointments = appointments.Where(a => a.isConfirmed == false && a.AppointmentDate.Date > DateTime.Now).OrderBy(a => a.AppointmentDate).ToList();
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

    private async void AppointmentsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is Appointment appointment)
        {
            var appointmentPage = new AppointmentPage(appointment, true);
            await Navigation.PushModalAsync(appointmentPage);
        }
        ((ListView)sender).SelectedItem = null;
    }
    private async Task<List<Appointment>> UpdateAppointments()
    {
        return await _shopService.GetAppointmentsByServiceId(int.Parse(await SecureStorage.Default.GetAsync("ServiceId")));
    }

}