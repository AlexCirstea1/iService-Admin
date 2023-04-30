using iService_Admin.Models;
using iService_Admin.Services;

namespace iService_Admin.Views;

public partial class AppointmentPage : ContentPage
{
    private Appointment _appointment;
    private readonly ShopService _shopService;
	public AppointmentPage(Appointment appointment)
	{
        _appointment = appointment;
		InitializeComponent();
        Load();
    }

    private void Load()
    {
        CarName.Text = _appointment.CarName;
        Type.Text = _appointment.AppointmentType;
        Notes.Text = _appointment.AppointmentNotes;
        Date.Text = _appointment.AppointmentDate.ToString();
        ClientName.Text = _appointment.Client;
        ClientEmail.Text = _appointment.User.Email;
    }

    private async void CanacelBtn_OnClicked(object sender, EventArgs e)
    {
        if (await DisplayAlert("Confirmation", "Are you sure you want to cancel ?", "Yes", "No"))
        {
            await _shopService.CancelAppointment(_appointment.AppointmentId);
        }
    }

    private void ContactBtn_OnClicked(object sender, EventArgs e)
    {
        
    }
}