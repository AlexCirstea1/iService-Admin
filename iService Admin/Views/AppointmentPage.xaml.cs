using System.Globalization;
using iService_Admin.Models;
using iService_Admin.Services;

namespace iService_Admin.Views;

public partial class AppointmentPage : ContentPage
{
    private Appointment _appointment;
    private readonly bool _pageType;
    private readonly ShopService _shopService = new();
	public AppointmentPage(Appointment appointment, bool pageType)
	{
        _pageType = pageType;
        _appointment = appointment;
		InitializeComponent();
        Load();
    }

    private void Load()
    {
        ConfirmBtn.IsVisible = _pageType;
        if (!_pageType)
        {
            grid.SetColumnSpan(ContactBtn,2);
        }
        UpdateAppointments();
        CarName.Text = _appointment.CarName;
        Type.Text = _appointment.AppointmentType;
        Notes.Text = _appointment.AppointmentNotes;
        Date.Text = _appointment.AppointmentDate.ToString(CultureInfo.CurrentCulture);
        ClientName.Text = _appointment.Client;
        ClientEmail.Text = _appointment.User.Email;
        isConfirmed.Text = _appointment.isConfirmed.ToString();
    }

    private async void UpdateAppointments()
    {
        var appointments = await _shopService.GetAppointmentsByServiceId(int.Parse(await SecureStorage.Default.GetAsync("ServiceId")));
        _appointment = appointments.FirstOrDefault(u => u.AppointmentId == _appointment.AppointmentId);
    }

    private async void CanacelBtn_OnClicked(object sender, EventArgs e)
    {
        if (await DisplayAlert("Confirmation", "Are you sure you want to cancel ?", "Yes", "No"))
        {
            await _shopService.CancelAppointment(_appointment.AppointmentId);
        }

        await Navigation.PopModalAsync();
    }

    private void ContactBtn_OnClicked(object sender, EventArgs e)
    {
        
    }

    private async void ConfirmBtn_OnClicked(object sender, EventArgs e)
    {
        await _shopService.ConfirmAppointment(_appointment.AppointmentId);
        Load();
        await Navigation.PopModalAsync();
    }
}