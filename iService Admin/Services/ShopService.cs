using System.Text;
using iService_Admin.Models;
using iService_Admin.Tools;
using Newtonsoft.Json;

namespace iService_Admin.Services
{
    class ShopService
    {
        private HttpConnectionServer _connection;
        private HttpClient _httpClient;

        public ShopService()
        {
            _connection = new HttpConnectionServer();
            _httpClient = _connection.GetHttpClient();
        }

        public async Task<Service> Login(string username, string password)
        {
            var requestData = new
            {
                email = username,
                password = password
            };

            var jsonContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            using var response = await _httpClient.PostAsync("api/Service/LoginService", content);

            var responseContent = await response.Content.ReadAsStringAsync();

            var serviceDeserializeObject = JsonConvert.DeserializeObject<Service>(responseContent);

            return serviceDeserializeObject;
        }

        public async Task<List<Appointment>> GetAppointmentsByServiceId(int serviceId)
        {
            var json = await _httpClient.GetStringAsync($"api/Appointment/GetAppointmentsByServiceId/{serviceId}");
            var appointments = JsonConvert.DeserializeObject<List<Appointment>>(json);
            return appointments;
        }

        public async Task CancelAppointment(int id)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"api/Appointment/CancelAppointment/{id}");
        }
    }
}
