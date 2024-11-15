using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;


namespace CITOGAU.ApiContext
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(string baseAddress)
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(baseAddress),
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        public async Task<UserResponse> LoginAsync(string login, string password)
        {
            var loginRequest = new LoginRequest
            {
                Login = login,
                Password = password
            };

            var json = JsonConvert.SerializeObject(loginRequest);
            Console.WriteLine($"Sending JSON: {json}");

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var userResponse = JsonConvert.DeserializeObject<UserResponse>(responseContent);
                return userResponse;
            }
            else
            {
                Console.WriteLine($"Login failed: {response.StatusCode}, {await response.Content.ReadAsStringAsync()}");
                return null;
            }
        }
    }
}
