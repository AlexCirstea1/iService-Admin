using iService_Admin.Models;
using iService_Admin.Services;
using Syncfusion.Maui.Calendar;

namespace iService_Admin.Views;

public partial class CalendarPage : ContentPage
{
    private readonly ShopService _shopService;
    public CalendarPage()
    {
        _shopService = new ShopService();
        InitializeComponent();
        Load(DateTime.Now.Date);
    }

    private async void Load(DateTime _date)
    {
        try
        {
            var serviceId = int.Parse(await SecureStorage.Default.GetAsync("ServiceId"));
            var appointments = await _shopService.GetAppointmentsByServiceId(serviceId);
            appointments = appointments.Where(a => a.AppointmentDate.Date == _date).OrderBy(a => a.AppointmentDate.TimeOfDay).ToList();
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

    private void SfCalendar_OnTapped(object sender, CalendarTappedEventArgs e)
    {
        Load(e.Date);
    }

    private async void AppointmentsListView_OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        if (e.Item is Appointment appointment)
        {
            var appointmentPage = new AppointmentPage(appointment, false);
            await Navigation.PushModalAsync(appointmentPage);
        }
        ((ListView)sender).SelectedItem = null;
    }
}