using iService_Admin.Models;
using iService_Admin.Services;

namespace iService_Admin;

public partial class AppShell : Shell
{
    private readonly ShopService _shopService;
    public AppShell()
	{
        _shopService = new ShopService();
        InitializeComponent();
        LoadImage();
	}
    private async void LoadImage()
    {
        var serviceId = int.Parse(await SecureStorage.Default.GetAsync("ServiceId"));
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
}
