using System.Text;
using iService_Admin.Models;
using iService_Admin.Tools;
using Newtonsoft.Json;

namespace iService_Admin.Services
{
    internal class ShopService
    {
        private readonly HttpConnectionServer _connection;
        private readonly HttpClient _httpClient;

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

            foreach (var VARIABLE in appointments)
            {
                VARIABLE.User = await GetUserById(VARIABLE.UserId);
                VARIABLE.Car = await GetCarById(VARIABLE.CarId);
            }

            return appointments;
        }

        public async Task<User> GetUserById(int id)
        {
            var response = await _httpClient.GetAsync($"api/User/GetUserById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<User>(responseString);
                return user;
            }
            else
            {
                throw new Exception($"Error getting user with id {id}: {response.ReasonPhrase}");
            }
        }

        public async Task<Car> GetCarById(int id)
        {
            var response = await _httpClient.GetAsync($"api/Car/GetCarById/{id}");
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var car = JsonConvert.DeserializeObject<Car>(responseString);
                return car;
            }
            else
            {
                throw new Exception($"Error getting car with id {id}: {response.ReasonPhrase}");
            }
        }
        public async Task CancelAppointment(int id)
        {
           var response = await _httpClient.DeleteAsync($"api/Appointment/CancelAppointment/{id}");
           Console.WriteLine(response);
        }

        public async Task<bool> ConfirmAppointment(int id)
        {
            var data = new
            {
                isConfirmed = true
            };
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/Appointment/ConfirmAppointment/{id}",content);

            return response.IsSuccessStatusCode;
        }
        public async Task<byte[]> GetImage(int serviceId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Service/GetServiceLogoById/{serviceId}");

                if (response.IsSuccessStatusCode)
                {
                    var imageBytes = await response.Content.ReadAsByteArrayAsync();
                    return imageBytes;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving the image: {ex.Message}");
                return null;
            }
        }
    }
}
